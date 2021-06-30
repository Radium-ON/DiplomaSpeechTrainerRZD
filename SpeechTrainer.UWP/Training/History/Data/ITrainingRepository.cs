using System.Threading.Tasks;
using SpeechTrainer.Core.ResponseWrapper;

namespace SpeechTrainer.UWP.Training.History.Data
{
    internal interface ITrainingRepository
    {
        Task<IResponseWrapper> GetTrainings(int idStudent);
    }
}
