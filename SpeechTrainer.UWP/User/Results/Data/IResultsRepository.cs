using System.Threading.Tasks;
using SpeechTrainer.Core.ResponseWrapper;

namespace SpeechTrainer.UWP.User.Results.Data
{
    internal interface IResultsRepository
    {
        Task<IResponseWrapper> GetTrainings(int idStudent);
    }
}
