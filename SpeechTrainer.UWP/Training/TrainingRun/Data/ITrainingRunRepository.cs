using System.Threading.Tasks;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.UWP.Training.TrainingRun.Data
{
    internal interface ITrainingRunRepository
    {
        Task<IResponseWrapper> GetForms(int idSituation);
        Task<IResponseWrapper> CreateTraining(int idStudent, SituationObservable situation, PositionObservable position, TrainingObservable newObject);
    }
}
