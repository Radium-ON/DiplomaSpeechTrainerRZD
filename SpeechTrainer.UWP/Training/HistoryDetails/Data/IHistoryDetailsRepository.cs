using System.Threading.Tasks;
using SpeechTrainer.Core.ResponseWrapper;

namespace SpeechTrainer.UWP.Training.HistoryDetails.Data
{
    internal interface IHistoryDetailsRepository
    {
        Task<IResponseWrapper> GetTrainingLines(int idTraining);
        Task<IResponseWrapper> GetPosition(int idTraining);
    }
}
