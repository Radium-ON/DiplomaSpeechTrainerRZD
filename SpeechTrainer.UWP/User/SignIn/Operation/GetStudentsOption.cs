using System.Threading.Tasks;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.UWP.User.SignIn.Data;

namespace SpeechTrainer.UWP.User.SignIn.Operation
{
    public class GetStudentsOption
    {
        private readonly ISignInRepository _repository;

        public GetStudentsOption()
        {
            _repository = new SignInRepository();
        }

        public async Task<IResponseWrapper> Get()
        {
            return await Task.Run(() => _repository.GetAllStudents());
        }
    }
}
