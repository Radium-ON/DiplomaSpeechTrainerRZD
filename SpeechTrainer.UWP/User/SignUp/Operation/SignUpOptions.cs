using System.Threading.Tasks;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.UWP.User.SignUp.Data;

namespace SpeechTrainer.UWP.User.SignUp.Operation
{
    public class SignUpOptions
    {
        private readonly ISignUpRepository _repository;

        public SignUpOptions()
        {
            _repository = new SignUpRepository();
        }

        public async Task<IResponseWrapper> GetGroups()
        {
            return await Task.Run(() => _repository.GetAllGroups());
        }

        public async Task<IResponseWrapper> CreateStudent(StudentObservable student)
        {
            return await Task.Run(() => _repository.CreateStudent(student));
        }
    }
}
