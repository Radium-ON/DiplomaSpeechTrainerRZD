using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using SpeechTrainer.Core;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.UWP.Training.History.Operation;

namespace SpeechTrainer.UWP.Training.History.View
{
    public class HistoryViewModel : ObservableRecipient
    {
        private readonly TrainingHistoryOptions _historyOptions;

        private TrainingObservable _selectedTraining;
        public TrainingObservable SelectedTraining
        {
            get => _selectedTraining;
            set => SetProperty(ref _selectedTraining, value, true);
        }

        public RelayCommand SendCommand { get; }

        private ObservableCollection<TrainingObservable> _trainings;
        private bool _loadingEnded;

        public ObservableCollection<TrainingObservable> Trainings
        {
            get => _trainings;
            set => SetProperty(ref _trainings, value);
        }

        public bool LoadingEnded
        {
            get => _loadingEnded;
            set => SetProperty(ref _loadingEnded, value);
        }

        public HistoryViewModel(TrainingHistoryOptions historyOptions)
        {
            _historyOptions = historyOptions;
            SendCommand = new RelayCommand(SendTrainingMessage);
        }

        public async Task InitializeProperties()
        {
            LoadingEnded = false;
            await GetTrainings();
            LoadingEnded = true;
        }

        private async Task GetTrainings()
        {
            try
            {
                var response = await _historyOptions.GetTrainings(Session.StudentId);
                if (response is Success<List<TrainingObservable>> responseWrapper)
                {
                    Trainings = new ObservableCollection<TrainingObservable>(responseWrapper.Data);
                }
                else
                {
                    var errorMessage = (response as Error)?.Message;
                    Debug.WriteLine("[HistoryViewModel.GetTrainings()] Error: " + errorMessage);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }

        public void SendTrainingMessage()
        {
            Messenger.Send(new ValueChangedMessage<TrainingObservable>(SelectedTraining));
        }
    }
}
