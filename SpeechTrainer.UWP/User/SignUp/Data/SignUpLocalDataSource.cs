using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Database;
using SpeechTrainer.Database.Entities;
using SpeechTrainer.Database.Interfaces;

namespace SpeechTrainer.UWP.User.SignUp.Data
{
    public class SignInLocalDataSource : ISignUpDataSource
    {
        private readonly IDataBaseStudent<StudentDto, bool> _dbStudent;
        private readonly IDataBaseGroup<GroupDto, bool> _dbGroup;

        public SignInLocalDataSource()
        {
            _dbStudent = new DataBaseStudent();
            _dbGroup = new DataBaseGroup();
        }

        #region Implementation of ISignUpDataSource

        public async Task<List<GroupDto>> GetAllGroups()
        {
            return await _dbGroup.SelectAllAsync(true);
        }

        public async Task<bool> CreateStudent(StudentDto student)
        {
            return await _dbStudent.CreateAsync(student);
        }

        #endregion
    }
}
