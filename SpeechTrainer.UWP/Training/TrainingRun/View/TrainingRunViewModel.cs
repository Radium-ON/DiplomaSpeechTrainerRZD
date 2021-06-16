using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using SpeechTrainer.Core;
using SpeechTrainer.Core.Interfaces;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.Core.Utills;
using SpeechTrainer.UWP.Training.TrainingRun.Operation;

namespace SpeechTrainer.UWP.Training.TrainingRun.View
{
    public class TrainingRunViewModel : ObservableRecipient
    {
        private readonly TrainingRunOptions _trainingRunOptions;
        private readonly TrainingService _trainingService;

        private TrainingObservable _training;
        private ObservableCollection<AnswerFormObservable> _answerForms;
        private PositionObservable _position;
        private bool _playAnimation;
        private bool _trainingAlreadyCreated;

        public TrainingRunViewModel(TrainingService trainingService, TrainingRunOptions trainingRunOptions)
        {
            _trainingRunOptions = trainingRunOptions;
            _trainingService = trainingService;
            _trainingService.TrainingEnded += async (o, args) => await TrainingServiceOnTrainingEnded(o, args);
            _trainingService.StepCompleted += async (o, i) => await TrainingServiceOnStepCompleted(o, i);

            Training = new TrainingObservable();
        }

        public event EventHandler ExitRequested;

        private async Task TrainingServiceOnStepCompleted(object sender, int e)
        {
        }

        private async Task TrainingServiceOnTrainingEnded(object sender, EventArgs e)
        {
            if (!_trainingAlreadyCreated)
            {
                await CreateTraining(_trainingService.Training);
            }
        }

        public IAsyncRelayCommand AnswerCommand => new AsyncRelayCommand(RecordAnswer);

        public IRelayCommand InterruptCommand => new RelayCommand(Exit);

        private void Exit()
        {
            _trainingService.StopTraining();
            ExitRequested?.Invoke(this, EventArgs.Empty);
        }

        private async Task RecordAnswer()
        {
            PlayAnimation = true;
            await _trainingService.RecordStudentAnswerAsync();
            PlayAnimation = false;
        }

        public bool PlayAnimation
        {
            get => _playAnimation;
            set => SetProperty(ref _playAnimation, value);
        }

        public TrainingObservable Training
        {
            get => _training;
            set => SetProperty(ref _training, value);
        }

        public PositionObservable Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        public string RecognitionInProcess => _trainingService.RecognitionInProcess;

        public ObservableCollection<AnswerFormObservable> AnswerForms
        {
            get => _answerForms;
            set => SetProperty(ref _answerForms, value);
        }

        public async Task InitializeProperties()
        {
            await GetAnswerFormsAsync();
            await _trainingService.TrainingRunAsync(Session.StudentId, Position, Training.Situation, AnswerForms.ToList());
        }

        private async Task GetAnswerFormsAsync()
        {
            var response = await _trainingRunOptions.GetFormsAsync(Training.Situation.Id);
            if (response is Success<List<AnswerFormObservable>> responseWrapper)
            {
                AnswerForms = new ObservableCollection<AnswerFormObservable>(responseWrapper.Data);
            }
            else
            {
                var errorMessage = (response as Error)?.Message;
                Debug.WriteLine("[TrainingRunViewModel.GetAnswerFormsAsync()] Error: " + errorMessage);
            }
        }

        private async Task CreateTraining(TrainingObservable resultTraining)
        {
            if (resultTraining.Situation != null && resultTraining.TrainingLines != null)
            {
                var response = await _trainingRunOptions.CreateTraining(Session.StudentId, resultTraining, Position);
                if (response is Success<bool> responseWrapper && responseWrapper.Data)
                {
                    _trainingAlreadyCreated = true;
                }
                else
                {
                    var errorMessage = (response as Error)?.Message;

                    Debug.WriteLine("[TrainingRunViewModel.CreateTraining()] Error: " + errorMessage);
                }
            }
        }

        #region Overrides of ObservableRecipient

        protected override void OnActivated()
        {
            Messenger.Register<TrainingRunViewModel, TrainingStartMessage>(this,
                (r, m) =>
                {
                    r.Position = m.Position;
                    r.Training.Situation = m.Value;
                });
        }

        #endregion
    }
}
