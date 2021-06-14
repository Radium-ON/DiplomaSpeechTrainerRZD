using System.Threading.Tasks;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.UWP.Training.HistoryDetails.Data;
using SpeechTrainer.UWP.Training.TrainingStart.Data;

namespace SpeechTrainer.UWP.Training.TrainingStart.Operation
{
    public class TrainingStartOptions
    {
        private readonly ITrainingStartRepository _repository;

        public TrainingStartOptions()
        {
            _repository = new TrainingStartRepository();
        }

        public async Task<IResponseWrapper> GetSituations()
        {
            return await Task.Run(() => _repository.GetSituations());
        }
    }
}
