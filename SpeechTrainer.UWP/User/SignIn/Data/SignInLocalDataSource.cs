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

        public SignInLocalDataSource()
        {
            _dataBase = new DataBaseStudent();
        }
        public async Task<List<StudentDto>> GetAllStudents()
        {
            return await _dataBase.SelectAllAsync();
        }
    }
}
