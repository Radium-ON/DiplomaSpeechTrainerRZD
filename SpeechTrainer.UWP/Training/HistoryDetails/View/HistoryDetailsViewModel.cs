using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.UWP.Training.HistoryDetails.Operation;

namespace SpeechTrainer.UWP.Training.HistoryDetails.View
{
    public class HistoryDetailsViewModel : ObservableRecipient
    {
        private readonly TrainingDetailsOptions _detailsOptions;

        private TrainingObservable _training;
        private PositionObservable _position;
        private ObservableCollection<TrainingLineObservable> _trainingLines;
        private bool _loadingEnded;

        public bool LoadingEnded
        {
            get => _loadingEnded;
            set => SetProperty(ref _loadingEnded, value);
        }

        public TrainingObservable Training
        {
            get => _training;
            set => SetProperty(ref _training, value);
        }

        public ObservableCollection<TrainingLineObservable> TrainingLines
        {
            get => _trainingLines;
            set => SetProperty(ref _trainingLines, value);
        }

        public PositionObservable Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        public HistoryDetailsViewModel(TrainingDetailsOptions detailsOptions)
        {
            _detailsOptions = detailsOptions;
        }

        public async Task InitializePropertiesAsync()
        {
            LoadingEnded = false;
            await GetPositionAsync();
            await GetTrainingLinesAsync();
            LoadingEnded = true;
        }

        private async Task GetTrainingLinesAsync()
        {
            var response = await _detailsOptions.GetTrainingLines(Training.Id);
            if (response is Success<List<TrainingLineObservable>> responseWrapper)
            {
                TrainingLines = new ObservableCollection<TrainingLineObservable>(responseWrapper.Data);
            }
            else
            {
                var errorMessage = (response as Error)?.Message;
                Debug.WriteLine("[HistoryDetailsViewModel.GetTrainingLinesAsync()] Error: " + errorMessage);
            }
        }

        private async Task GetPositionAsync()
        {
            var response = await _detailsOptions.GetPosition(Training.Id);
            if (response is Success<PositionObservable> responseWrapper)
            {
                Position = responseWrapper.Data;
            }
            else
            {
                var errorMessage = (response as Error)?.Message;
                Debug.WriteLine("[HistoryDetailsViewModel.GetPositionAsync()] Error: " + errorMessage);
            }
        }

        #region Overrides of ObservableRecipient

        protected override void OnActivated()
        {
            Messenger.Register<HistoryDetailsViewModel, ValueChangedMessage<TrainingObservable>>(this,
                (r, m) => r.Training = m.Value);
        }

        #endregion
    }
}
