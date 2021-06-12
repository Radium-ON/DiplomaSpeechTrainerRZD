using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SpeechTrainer.Core;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.Core.Utills;
using SpeechTrainer.UWP.User.SignIn.Operation;

namespace SpeechTrainer.UWP.User.SignIn.View
{
    public class SignInViewModel : ObservableObject
    {
        private readonly GetStudentsOption _getStudentsOption;
        private ObservableCollection<StudentObservable> _students;
        private bool _visibleLoading;
        private StudentObservable _student;

        public StudentObservable Student
        {
            get => _student;
            set => SetProperty(ref _student, value);
        }

        public ICommand SignInCommand => new RelayCommand(() => SignIn(_student));

        public bool VisibleLoading
        {
            get => _visibleLoading;
            set => SetProperty(ref _visibleLoading, value);
        }
        public ObservableCollection<StudentObservable> Students
        {
            get => _students;
            set => SetProperty(ref _students, value);
        }

        public SignInViewModel(GetStudentsOption getStudentsOption)
        {
            this._getStudentsOption = getStudentsOption;
            GetAllStudentsAsync();
        }

        private async Task GetAllStudentsAsync()
        {
            var response = await _getStudentsOption.Get();
            if (response is Success<List<StudentObservable>> responseWrapper)
            {
                Students = new ObservableCollection<StudentObservable>(responseWrapper.Data);
            }
            else
            {
                var errorMessage = (response as Error)?.Message;
                Debug.WriteLine("[SignInViewModel.GetAllStudentsAsync()] Error: " + errorMessage);
            }
        }

        private void SignIn(StudentObservable student)
        {
            Session.SetId(student.Id);
        }

        private void ShowLoadingData(bool value)
        {
            VisibleLoading = value;
        }

    }
}
