using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public PositionObservable Position { get; private set; }
        public TrainingObservable Training { get; private set; }

        public string RecognitionInProcess { get; set; }

        public event EventHandler TrainingEnded;
        public event EventHandler<int> StepCompleted;
        public event EventHandler RecognitionStarted;

        public TrainingService(ISpeechToText<RecognitionResult> speechService)
        {
            _speechService = speechService as SpeechService;
            SetSpeechServiceEvents();
        }

        private void SetSpeechServiceEvents()
        {
            _speechService.SynthesizerSynthesisCompleted += SpeechServiceOnSynthesizerSynthesisCompleted;
            _speechService.MediaPlaybackEnded += SpeechServiceOnMediaPlaybackEnded;

            _speechService.RecognizerRecognized += SpeechServiceOnRecognizerRecognized;
            _speechService.RecognizerRecognizing += SpeechServiceOnRecognizerRecognizing;
            _speechService.RecognizerSessionStopped += SpeechServiceOnRecognizerSessionStopped;
            _speechService.RecognizerSessionStarted += SpeechServiceOnRecognizerSessionStarted;
        }

        private void ResetCounters()
        {
            _allStudentPhrases = 0;
            _correctStudentPhrases = 0;
        }

        private async void SpeechServiceOnMediaPlaybackEnded(object sender, EventArgs e)
        {
            StepCompleted?.Invoke(this, _answerForms.Count);
            IsTrainingEnd();
            if (IsNextPhraseSynth(_answerForms))
            {
                await DoSituationStepAsync(_answerForms);
            }
        }

        private void SpeechServiceOnRecognizerSessionStarted(object sender, SessionEventArgs e)
        {
            RecognitionStarted?.Invoke(this, null);
        }

        private void SpeechServiceOnRecognizerRecognizing(object sender, SpeechRecognitionEventArgs e)
        {
            RecognitionInProcess = e.Result.Text;
        }

        private async void SpeechServiceOnRecognizerSessionStopped(object sender, SessionEventArgs e)
        {
            StepCompleted?.Invoke(this, _answerForms.Count);
            if (!IsTrainingEnd())
            {
                await DoSituationStepAsync(_answerForms);
            }
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

        private async void SpeechServiceOnSynthesizerSynthesisCompleted(object sender, SpeechSynthesisEventArgs e)
        {
            // StepCompleted?.Invoke(this, _answerForms.Count);
            // IsTrainingEnd();
            // if (IsNextPhraseSynth(_answerForms))
            // {
            //     await DoSituationStepAsync(_answerForms);
            // }
        }

        private bool IsNextPhraseSynth(List<AnswerFormObservable> forms)
        {
            _currentForm = forms.First();
            return !CheckPositionStudent(_currentForm?.Position, Position);
        }

        private bool IsTrainingEnd()
        {
            if (_answerForms.Count > 0)
            {
                return false;
            }

            CalcScores();
            TrainingEnded?.Invoke(this, null);
            return true;
        }

        public void StopTraining()
        {
            _speechService.StopOperations();
        }

        public async Task RecordStudentAnswerAsync()
        {
            if (!IsTrainingEnd())
            {
                await DoSituationStepAsync(_answerForms);
            }
        }

        public async Task TrainingRunAsync(int studentId, PositionObservable position, SituationObservable situation, List<AnswerFormObservable> forms)
        {
            ResetCounters();
            Training = new TrainingObservable { TrainingLines = new List<TrainingLineObservable>(), StudentId = studentId, Situation = situation, TrainingDate = DateTime.Now };
            Position = position;
            _answerForms = forms.OrderBy(n => n.OrderNum).ToList();

            await DoSituationStepAsync(_answerForms);
        }

        private async Task DoSituationStepAsync(List<AnswerFormObservable> forms)
        {
            _currentForm = forms.First();
            _answerForms.RemoveAt(0);
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

        private void CalcScores()
        {
            Training.ScoresNumber = (int)(_correctStudentPhrases * 100.0 / _allStudentPhrases);
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

        private bool CheckPositionStudent(PositionObservable formPosition, PositionObservable studentPosition)
        {
            return formPosition.Id == studentPosition.Id;
        }

        private string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, @"[^\w\d\s+]", "").ToUpper().Replace(" ", string.Empty);
        }

    }
}
