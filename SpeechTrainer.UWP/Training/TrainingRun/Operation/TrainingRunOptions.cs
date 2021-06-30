using System.Threading.Tasks;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.UWP.Training.TrainingRun.Data;

namespace SpeechTrainer.UWP.Training.TrainingRun.Operation
{
    public class TrainingRunOptions
    {
        private readonly ITrainingRunRepository _repository;

        public TrainingRunOptions()
        {
            _repository = new TrainingRunRepository();
        }

        public async Task<IResponseWrapper> GetFormsAsync(int idSituation)
        {
            return await Task.Run(() => _repository.GetForms(idSituation));
        }

        public async Task<IResponseWrapper> CreateTraining(int studentId, TrainingObservable training, PositionObservable position)
        {
            return await Task.Run(() => _repository.CreateTraining(studentId, training.Situation, position, training));
        }
    }
}
