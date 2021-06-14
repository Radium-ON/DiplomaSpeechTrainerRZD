using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SpeechTrainer.Core.DtoMappers;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;

namespace SpeechTrainer.UWP.User.SignUp.Data
{
    public class SignUpRepository : ISignUpRepository
    {
        private readonly ISignUpDataSource _localDataSource;

        public SignUpRepository()
        {
            _localDataSource = new SignInLocalDataSource();
        }

        #region Implementation of ISignUpRepository

        public async Task<IResponseWrapper> GetAllGroups()
        {
            try
            {
                var response = await _localDataSource.GetAllGroups();
                var list = GroupMapper.ConvertFromListDto(response).ToList();
                return new Success<List<GroupObservable>>(list);
            }
            catch (Exception e)
            {
                Debug.WriteLine("[SignUpRepository.GetAllGroups()] Error: " + e.Message);
                return new Error(e.Message);
            }
        }

        public async Task<IResponseWrapper> CreateStudent(StudentObservable student)
        {
            try
            {
                var response = await _localDataSource.CreateStudent(StudentMapper.ConvertToDto(student));
                return new Success<bool>(response);
            }
            catch (Exception e)
            {
                Debug.WriteLine("[SignUpRepository.CreateStudent()] Error: " + e.Message);
                return new Error(e.Message);
            }
        }

        #endregion
    }
}
