using System.Threading.Tasks;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.UWP.Training.HistoryDetails.Data;

namespace SpeechTrainer.UWP.Training.HistoryDetails.Operation
{
    public class TrainingDetailsOptions
    {
        private readonly IHistoryDetailsRepository _repository;

        public TrainingDetailsOptions()
        {
            _repository = new HistoryDetailsRepository();
        }

        public async Task<IResponseWrapper> GetTrainingLines(int idTraining)
        {
            return await Task.Run(() => _repository.GetTrainingLines(idTraining));
        }

        public async Task<IResponseWrapper> GetPosition(int idTraining)
        {
            return await Task.Run(() => _repository.GetPosition(idTraining));
        }
    }
}
