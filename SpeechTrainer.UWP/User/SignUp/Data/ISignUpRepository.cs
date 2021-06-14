using System.Threading.Tasks;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;

namespace SpeechTrainer.UWP.User.SignUp.Data
{
    internal interface ISignUpRepository
    {
        Task<IResponseWrapper> GetAllGroups();
        Task<IResponseWrapper> CreateStudent(StudentObservable student);
    }
}
