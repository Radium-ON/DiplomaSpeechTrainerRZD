using System.Threading.Tasks;
using SpeechTrainer.Core.ResponseWrapper;

namespace SpeechTrainer.UWP.Training.TrainingStart.Data
{
    internal interface ITrainingStartRepository
    {
        Task<IResponseWrapper> GetSituations();
    }
}
