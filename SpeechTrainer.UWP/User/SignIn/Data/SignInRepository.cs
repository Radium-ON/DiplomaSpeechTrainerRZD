using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SpeechTrainer.Core.DtoMappers;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;

namespace SpeechTrainer.UWP.User.SignIn.Data
{
    public class SignInRepository : ISignInRepository
    {
        private readonly ISignInDataSource _localDataSource;

        public SignInRepository()
        {
            _localDataSource = new SignInLocalDataSource();
        }
        public async Task<IResponseWrapper> GetAllStudents()
        {
            try
            {
                var response = await _localDataSource.GetAllStudents();
                var list = StudentMapper.ConvertFromListDto(response).ToList();
                return new Success<List<StudentObservable>>(list);
            }
            catch (Exception e)
            {
                Debug.WriteLine("[SignInRepository.GetAllStudents()] Error: " + e.Message);
                return new Error(e.Message);
            }
        }
    }
}
