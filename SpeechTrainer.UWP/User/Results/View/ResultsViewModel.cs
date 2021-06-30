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

        private List<TrainingObservable> _trainings;
        private bool _loadingEnded;
        private int _allAttempts;
        private int _excellentCount;
        private int _couldBeBetterCount;
        private double _correctAnswersRatio;
        private SituationObservable _problemSituation;

        public bool LoadingEnded
        {
            get => _loadingEnded;
            set => SetProperty(ref _loadingEnded, value);
        }

        public int AllAttempts
        {
            get => _allAttempts;
            set => SetProperty(ref _allAttempts, value);
        }

        public int ExcellentCount
        {
            get => _excellentCount;
            set => SetProperty(ref _excellentCount, value);
        }

        public int CouldBeBetterCount
        {
            get => _couldBeBetterCount;
            set => SetProperty(ref _couldBeBetterCount, value);
        }

        public double CorrectAnswersRatio
        {
            get => _correctAnswersRatio;
            set => SetProperty(ref _correctAnswersRatio, value);
        }

        public SituationObservable ProblemSutiation
        {
            get => _problemSituation;
            set => SetProperty(ref _problemSituation, value);
        }

        public List<TrainingObservable> Trainings
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
            await _analytics.CollectAnalyticsAsync(Trainings);
            AllAttempts = _analytics.AllAttempts;
            ExcellentCount = _analytics.ExcellentCount;
            CouldBeBetterCount = _analytics.CouldBeBetterCount;
            CorrectAnswersRatio = _analytics.CorrectAnswersRatio;
            ProblemSutiation = _analytics.ProblemSituation;
            LoadingEnded = true;
        }

        private async Task GetTrainings()
        {
            try
            {
                var response = await _resultsOptions.GetTrainings(Session.StudentId);
                if (response is Success<List<TrainingObservable>> responseWrapper)
                {
                    Trainings = new List<TrainingObservable>(responseWrapper.Data);
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
