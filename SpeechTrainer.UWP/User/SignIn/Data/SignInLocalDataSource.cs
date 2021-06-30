using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Database;
using SpeechTrainer.Database.Entities;
using SpeechTrainer.Database.Interfaces;

namespace SpeechTrainer.UWP.User.SignIn.Data
{
    public class SignInLocalDataSource : ISignInDataSource
    {
        private readonly IDataBaseStudent<StudentDto, bool> _dataBase;

        public Exception DbException { get; set; }

        public SignInLocalDataSource()
        {
            _dataBase = new DataBaseStudent();
        }
        public async Task<List<StudentDto>> GetAllStudents()
        {
            var result = await _dataBase.SelectAllAsync();
            DbException = (_dataBase as DataBaseStudent)?.GetRootException();
            return result;
        }
    }
}
