using System.Threading.Tasks;
using SpeechTrainer.Core.ResponseWrapper;

namespace SpeechTrainer.UWP.User.SignIn.Data
{
    internal interface ISignInRepository
    {
        Task<IResponseWrapper> GetAllStudents();
    }
}
