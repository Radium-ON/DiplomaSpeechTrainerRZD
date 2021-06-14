using System.Threading.Tasks;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.UWP.Training.History.Data;
using SpeechTrainer.UWP.User.SignUp.Data;

namespace SpeechTrainer.UWP.Training.History.Operation
{
    public class TrainingHistoryOptions
    {
        private readonly ITrainingRepository _repository;

        public TrainingHistoryOptions()
        {
            _repository = new TrainingRepository();
        }

        public async Task<IResponseWrapper> GetTrainings(int idStudent)
        {
            return await Task.Run(() => _repository.GetTrainings(idStudent));
        }
    }
}
