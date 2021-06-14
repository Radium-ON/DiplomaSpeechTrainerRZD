using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.UWP.User.SignUp.Data
{
    interface ISignUpDataSource
    {
        Task<List<GroupDto>> GetAllGroups();
        Task<bool> CreateStudent(StudentDto student);
        
    }
}
