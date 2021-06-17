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

        public StudentObservable Student
        {
            get => _student;
            set
            {
                SetProperty(ref _student, value);
                OnPropertyChanged(nameof(SignInCommand));
            }
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
                var errorMessage = (response as Error)?.Message;
                Debug.WriteLine("[SignInViewModel.GetAllStudentsAsync()] Error: " + errorMessage);
            }
        }

        private void SignIn(StudentObservable student)
        {
            Session.SetId(student.Id);
        }

    }
}
