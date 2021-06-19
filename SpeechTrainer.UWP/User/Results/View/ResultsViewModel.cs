using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SpeechTrainer.Core;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.Core.Utills;
using SpeechTrainer.UWP.Training.History.Operation;
using SpeechTrainer.UWP.User.Results.Operation;

namespace SpeechTrainer.UWP.User.Results.View
{
    public class ResultsViewModel : ObservableObject
    {
        private readonly ResultsOptions _resultsOptions;
        private readonly AnalyticsService _analytics;

        private ObservableCollection<TrainingObservable> _trainings;
        private bool _loadingEnded;

        public bool LoadingEnded
        {
            get => _loadingEnded;
            set => SetProperty(ref _loadingEnded, value);
        }

        public ObservableCollection<TrainingObservable> Trainings
        {
            get => _trainings;
            set => SetProperty(ref _trainings, value);
        }

        public ResultsViewModel(ResultsOptions resultsOptions, AnalyticsService analytics)
        {
            _resultsOptions = resultsOptions;
            _analytics = analytics;
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
                var response = await _resultsOptions.GetTrainings(Session.StudentId);
                if (response is Success<List<TrainingObservable>> responseWrapper)
                {
                    Trainings = new ObservableCollection<TrainingObservable>(responseWrapper.Data);
                }
                else
                {
                    var errorMessage = (response as Error)?.Message;
                    Debug.WriteLine("[ResultsViewModel.GetTrainings()] Error: " + errorMessage);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }
    }
}
