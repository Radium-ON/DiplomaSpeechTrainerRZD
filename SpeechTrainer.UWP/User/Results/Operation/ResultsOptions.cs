using System.Threading.Tasks;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.UWP.Training.History.Data;
using SpeechTrainer.UWP.User.Results.Data;

namespace SpeechTrainer.UWP.User.Results.Operation
{
    public class ResultsOptions
    {
        private readonly IResultsRepository _repository;

        public ResultsOptions()
        {
            _repository = new ResultsRepository();
        }

        public async Task<IResponseWrapper> GetTrainings(int idStudent)
        {
            return await Task.Run(() => _repository.GetTrainings(idStudent));
        }
    }
}
