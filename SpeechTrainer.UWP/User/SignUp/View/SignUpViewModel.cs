using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.Core.Utills;
using SpeechTrainer.UWP.User.SignIn.Operation;
using SpeechTrainer.UWP.User.SignUp.Operation;

namespace SpeechTrainer.UWP.User.SignUp.View
{
    public class SignUpViewModel : ObservableObject
    {
        private readonly SignUpOptions _signUpOptions;
        private ObservableCollection<GroupObservable> _groups;
        private GroupObservable _selectedGroup;
        private string _firstName;
        private string _lastName;
        private bool _showError;
        private string _studentCode;

        public string FirstName
        {
            get => _firstName;
            set { SetProperty(ref _firstName, value); OnPropertyChanged(nameof(SignUpCommand)); }
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string StudentCode
        {
            get => _studentCode;
            set => SetProperty(ref _studentCode, value);
        }

        public GroupObservable SelectedGroup
        {
            get => _selectedGroup;
            set => SetProperty(ref _selectedGroup, value);
        }

        public RelayCommand SignUpCommand => new RelayCommand(async () => await CreateStudent());

        public ObservableCollection<GroupObservable> Groups
        {
            get => _groups;
            set => SetProperty(ref _groups, value);
        }

        public bool ShowError
        {
            get => _showError;
            set => SetProperty(ref _showError, value);
        }

        public SignUpViewModel(SignUpOptions signUpOptions)
        {
            _signUpOptions = signUpOptions;
        }

        public async Task GetAllGroupsAsync()
        {

            var response = await _signUpOptions.GetGroups();
            if (response is Success<List<GroupObservable>> responseWrapper)
            {
                Groups = new ObservableCollection<GroupObservable>(responseWrapper.Data);
            }
            else
            {
                var errorMessage = (response as Error)?.Message;
                Debug.WriteLine("[SignUpViewModel.GetAllGroupsAsync()] Error: " + errorMessage);
            }

        }

        private async Task CreateStudent()
        {
            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && SelectedGroup != null && !string.IsNullOrEmpty(StudentCode))
            {
                var response = await _signUpOptions.CreateStudent(new StudentObservable(FirstName, LastName, SelectedGroup, StudentCode));
                if (response is Success<bool> responseWrapper && responseWrapper.Data)
                {
                    ShowError = false;
                }
                else
                {
                    var errorMessage = (response as Error)?.Message;

                    Debug.WriteLine("[SignUpViewModel.CreateStudent()] Error: " + errorMessage);
                }
            }
            else
            {
                ShowError = true;
            }
        }
    }
}
