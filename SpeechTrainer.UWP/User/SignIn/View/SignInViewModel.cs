using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SpeechTrainer.Core;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.UWP.User.SignIn.Operation;

namespace SpeechTrainer.UWP.User.SignIn.View
{
    public class SignInViewModel : ObservableObject
    {
        private readonly GetStudentsOption _getStudentsOption;
        private ObservableCollection<StudentObservable> _students;
        private StudentObservable _student;
        private string _errorMessage;
        private bool _openInfo;

        public StudentObservable Student
        {
            get => _student;
            set
            {
                SetProperty(ref _student, value);
                OnPropertyChanged(nameof(SignInCommand));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                SetProperty(ref _errorMessage, value);
                ShowError(_errorMessage);
            }
        }

        public void ShowError(string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                OpenInfo = false;
            }
            else
            {
                OpenInfo = true;
            }
        }

        public bool OpenInfo
        {
            get => _openInfo;
            set => SetProperty(ref _openInfo, value);
        }

        public RelayCommand<StudentObservable> SignInCommand => new RelayCommand<StudentObservable>(SignIn, d => Student != null);

        public ObservableCollection<StudentObservable> Students
        {
            get => _students;
            set => SetProperty(ref _students, value);
        }

        public SignInViewModel(GetStudentsOption getStudentsOption)
        {
            _getStudentsOption = getStudentsOption;
        }

        public async Task GetAllStudentsAsync()
        {
            var response = await _getStudentsOption.Get();
            if (response is Success<List<StudentObservable>> responseWrapper)
            {
                Students = new ObservableCollection<StudentObservable>(responseWrapper.Data);
            }
            else
            {
                ErrorMessage = (response as Error)?.Message;
                Debug.WriteLine("[SignInViewModel.GetAllStudentsAsync()] Error: " + ErrorMessage);
            }
        }

        private void SignIn(StudentObservable student)
        {
            Session.SetId(student.Id);
        }

    }
}
