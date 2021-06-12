using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.UWP.User.SignIn.Data
{
    interface ISignInDataSource
    {
        Task<List<StudentDto>> GetAllStudents();
        
    }
}
