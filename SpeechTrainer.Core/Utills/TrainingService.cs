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

        public StudentObservable Student { get; private set; }
        public PositionObservable Position { get; private set; }
        public TrainingObservable Training { get; private set; }

        public event EventHandler TrainingEnded;
        public event EventHandler<int> StepCompleted;

        public TrainingService(ISpeechToText<RecognitionResult> speechService)
        {
            _speechService = speechService as SpeechService;
            SetSpeechServiceEvents();
        }

        private void SetSpeechServiceEvents()
        {
            _speechService.SynthesizerSynthesisCompleted += _speechService_SynthesizerSynthesisCompleted;

            _speechService.RecognizerRecognized += _speechService_RecognizerRecognized;
            _speechService.RecognizerSessionStopped += _speechService_RecognizerSessionStopped;
        }

        private async void _speechService_RecognizerSessionStopped(object sender, SessionEventArgs e)
        {
            await DoSituationStepAsync(_answerForms);
        }

        private void _speechService_RecognizerRecognized(object sender, SpeechRecognitionEventArgs e)
        {
            var studentAnswer = e.Result.Text;
            var completeForm = GetCompleteAnswerForm(_currentForm);

            var line = new TrainingLineObservable
            {
                CompleteForm = completeForm,
                StudentAnswer = studentAnswer,
                IsCorrect = CompareAnswers(completeForm, studentAnswer)
            };
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
                var sortedParms = form.Parameters.OrderBy(n => n.OrderNum).Select(i => i.Value.Value).ToArray();
                textWithParms = string.Format(phraseText, sortedParms as object[]);
            }
            else
            {
                textWithParms = phraseText;
            }

            return textWithParms;
        }

        private void _speechService_SynthesizerSynthesisCompleted(object sender, SpeechSynthesisEventArgs e)
        {
            StepCompleted?.Invoke(this, _answerForms.Count);
        }

        public async Task RecordStudentAnswerAsync()
        {
            await DoSituationStepAsync(_answerForms);
        }

        public async Task TrainingRunAsync(StudentObservable student, PositionObservable position, SituationObservable situation, List<AnswerFormObservable> forms)
        {
            Training = new TrainingObservable { TrainingLines = new List<TrainingLineObservable>(), Student = student, Situation = situation };
            Student = student;
            Position = position;
            _answerForms = forms.OrderBy(n => n.OrderNum).ToList();

            await DoSituationStepAsync(_answerForms);
        }

        private async Task DoSituationStepAsync(List<AnswerFormObservable> forms)
        {
            _currentForm = forms.FirstOrDefault();
            if (_currentForm == null)
            {
                TrainingEnded?.Invoke(this, null);
                return;
            }
            if (CheckPositionStudent(_currentForm?.Position, Position))
            {
                var result = await _speechService.RecognizeSpeechFromMicrophoneAsync();
                Debug.WriteLine(result.Text);
            }
            else
            {
                await _speechService.GenerateSpeechToSpeakersAsync(_currentForm.Phrase.Text);
            }
            forms.RemoveAt(0);
        }

        private bool CheckPositionStudent(PositionObservable formPosition, PositionObservable studentPosition)
        {
            return formPosition.ShortName == studentPosition.ShortName;
        }

        private string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, @"[^\w\d\s]", "").ToUpper();
        }

    }
}
