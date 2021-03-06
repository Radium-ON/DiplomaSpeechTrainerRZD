using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SpeechTrainer.Core.Interfaces;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.Core.Utills;
using SpeechTrainer.UWP.Training.TrainingStart.Operation;

namespace SpeechTrainer.UWP.Training.TrainingStart.View
{
    public class TrainingStartViewModel : ObservableRecipient
    {
        private readonly TrainingStartOptions _trainingStartOptions;
        private readonly IPrivacySettings _privacySettingsEnabler;
        private ObservableCollection<SituationObservable> _situations;
        private SituationObservable _selectedSituation;
        private PositionObservable _selectedPosition;

        public TrainingStartViewModel(TrainingStartOptions trainingStartOptions, IPrivacySettings privacySettingsEnabler)
        {
            _trainingStartOptions = trainingStartOptions;
            _privacySettingsEnabler = privacySettingsEnabler;
        }

        public event EventHandler TrainingStarted;

        public IAsyncRelayCommand<PositionObservable> StartCommand => new AsyncRelayCommand<PositionObservable>(StartTraining,
            p => SelectedPosition != null && SelectedSituation != null);

        public ObservableCollection<SituationObservable> Situations
        {
            get => _situations;
            set => SetProperty(ref _situations, value);
        }

        public SituationObservable SelectedSituation
        {
            get => _selectedSituation;
            set => SetProperty(ref _selectedSituation, value, true);
        }

        public PositionObservable SelectedPosition
        {
            get => _selectedPosition;
            set
            {
                SetProperty(ref _selectedPosition, value, true);
                OnPropertyChanged(nameof(StartCommand));
            }
        }

        public async Task GetSituationsAsync()
        {
            var response = await _trainingStartOptions.GetSituations();
            if (response is Success<List<SituationObservable>> responseWrapper)
            {
                Situations = new ObservableCollection<SituationObservable>(responseWrapper.Data);
            }
            else
            {
                var errorMessage = (response as Error)?.Message;
                Debug.WriteLine("[TrainingStartViewModel.GetSituationsAsync()] Error: " + errorMessage);
            }
        }

        private async Task StartTraining(PositionObservable positionObservable)
        {
            await _privacySettingsEnabler.EnableMicrophoneAsync();
            TrainingStarted?.Invoke(this, EventArgs.Empty);
            Messenger.Send(new TrainingStartMessage(SelectedSituation, SelectedPosition));
        }
    }
}
