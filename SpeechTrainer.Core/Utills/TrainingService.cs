using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

        public StudentObservable Student { get; private set; }
        public PositionObservable Position { get; private set; }
        public TrainingObservable Training { get; private set; }

        public TrainingService(ISpeechToText<RecognitionResult> speechService)
        {
            _speechService = speechService as SpeechService;

        }

        public async Task TrainingRunAsync(StudentObservable student, PositionObservable position, List<AnswerFormObservable> forms)
        {
            Student = student;
            Position = position;
            _answerForms = forms.OrderBy(n => n.OrderNum).ToList();
            await DoSituationStepAsync(_answerForms.First());
        }

        private async Task DoSituationStepAsync(AnswerFormObservable form)
        {
            if (CheckPositionStudent(form.Position, Position))
            {
                var result = await _speechService.RecognizeSpeechFromMicrophoneAsync();
                Debug.WriteLine(result.Text);
            }
            else
            {
                await _speechService.GenerateSpeechToSpeakersAsync(form.Phrase.Text);
            }


        }

        private bool CheckPositionStudent(PositionObservable formPosition, PositionObservable studentPosition)
        {
            return formPosition.ShortName == studentPosition.ShortName;
        }
    }
}
