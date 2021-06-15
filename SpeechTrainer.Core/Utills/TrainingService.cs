using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using SpeechTrainer.Core.Interfaces;
using SpeechTrainer.Core.ModelObservable;

namespace SpeechTrainer.Core.Utills
{
    public class TrainingService
    {
        private readonly SpeechService _speechService;

        private List<AnswerFormObservable> _answerForms;
        private AnswerFormObservable _currentForm;
        private int _allStudentPhrases;
        private int _correctStudentPhrases;

        public StudentObservable Student { get; private set; }
        public PositionObservable Position { get; private set; }
        public TrainingObservable Training { get; private set; }

        public string RecognitionInProcess { get; set; }

        public event EventHandler TrainingEnded;
        public event EventHandler<int> StepCompleted;

        public TrainingService(ISpeechToText<RecognitionResult> speechService)
        {
            _speechService = speechService as SpeechService;
            SetSpeechServiceEvents();
        }

        private void SetSpeechServiceEvents()
        {
            _speechService.SynthesizerSynthesisCompleted += SpeechServiceOnSynthesizerSynthesisCompleted;

            _speechService.RecognizerRecognized += SpeechServiceOnRecognizerRecognized;
            _speechService.RecognizerRecognizing += SpeechServiceOnRecognizerRecognizing;
            _speechService.RecognizerSessionStopped += SpeechServiceOnRecognizerSessionStopped;
        }

        private void SpeechServiceOnRecognizerRecognizing(object sender, SpeechRecognitionEventArgs e)
        {
            RecognitionInProcess = e.Result.Text;
        }

        private async void SpeechServiceOnRecognizerSessionStopped(object sender, SessionEventArgs e)
        {
            await CheckTrainingEnd();
        }

        private async Task CheckTrainingEnd()
        {
            if (_answerForms.Count > 1)
            {
                _answerForms.RemoveAt(0);
                await DoSituationStepAsync(_answerForms);
            }
            else
            {
                CalcScores();
                TrainingEnded?.Invoke(this, null);
            }
        }

        public void StopTraining()
        {
            _speechService.StopOperations();
        }

        private void CalcScores()
        {
            Training.ScoresNumber = (int)(_correctStudentPhrases / (double)_allStudentPhrases * 100);
        }

        private void SpeechServiceOnRecognizerRecognized(object sender, SpeechRecognitionEventArgs e)
        {
            var studentAnswer = e.Result.Text;
            var completeForm = GetCompleteAnswerForm(_currentForm);

            var line = new TrainingLineObservable
            {
                CompleteForm = completeForm,
                StudentAnswer = studentAnswer,
                IsCorrect = CompareAnswers(completeForm, studentAnswer)
            };
            if (line.IsCorrect)
            {
                _correctStudentPhrases++;
            }
            Training.TrainingLines.Add(line);
            StepCompleted?.Invoke(this, _answerForms.Count);
        }

        private bool CompareAnswers(string phraseText, string studentText)
        {
            var cleanPhrase = RemoveSpecialCharacters(phraseText);
            var cleanAnswer = RemoveSpecialCharacters(studentText);
            return string.Equals(cleanPhrase, cleanAnswer);
        }

        private string GetCompleteAnswerForm(AnswerFormObservable form)
        {
            string textWithParms;
            var phraseText = form.Phrase.Text;
            if (form.Parameters.Count != 0)
            {
                object[] sortedParms = form.Parameters.OrderBy(n => n.OrderNum).Select(i => i.Value.Value).ToArray();
                textWithParms = string.Format(phraseText, sortedParms);
            }
            else
            {
                textWithParms = phraseText;
            }

            return textWithParms;
        }

        private void SpeechServiceOnSynthesizerSynthesisCompleted(object sender, SpeechSynthesisEventArgs e)
        {
            StepCompleted?.Invoke(this, _answerForms.Count);
        }

        public async Task RecordStudentAnswerAsync()
        {
            if (_answerForms.Count > 1)
            {
                _answerForms.RemoveAt(0);
                await DoSituationStepAsync(_answerForms);
            }
            else
            {
                TrainingEnded?.Invoke(this, null);
            }
        }

        public async Task TrainingRunAsync(int studentId, PositionObservable position, SituationObservable situation, List<AnswerFormObservable> forms)
        {
            Training = new TrainingObservable { TrainingLines = new List<TrainingLineObservable>(), StudentId = studentId, Situation = situation, TrainingDate = DateTime.Now };
            Position = position;
            _answerForms = forms.OrderBy(n => n.OrderNum).ToList();

            await DoSituationStepAsync(_answerForms);
        }

        private async Task DoSituationStepAsync(List<AnswerFormObservable> forms)
        {
            _currentForm = forms.First();

            if (CheckPositionStudent(_currentForm?.Position, Position))
            {
                _allStudentPhrases++;
                var result = await _speechService.RecognizeSpeechFromMicrophoneAsync();
                Debug.WriteLine(result.Text);
            }
            else
            {
                await _speechService.GenerateSpeechToSpeakersAsync(GetCompleteAnswerForm(_currentForm));
            }
        }

        private bool CheckPositionStudent(PositionObservable formPosition, PositionObservable studentPosition)
        {
            return formPosition.Id == studentPosition.Id;
        }

        private string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, @"[^\w\d\s]", "").ToUpper();
        }

    }
}
