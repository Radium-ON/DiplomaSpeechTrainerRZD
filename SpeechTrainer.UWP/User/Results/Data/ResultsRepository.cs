using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SpeechTrainer.Core.DtoMappers;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.UWP.Training.History.Data;

namespace SpeechTrainer.UWP.User.Results.Data
{
    public class ResultsRepository : IResultsRepository
    {
        private readonly IResultsDataSource _localDataSource;

        public ResultsRepository()
        {
            _localDataSource = new ResultsLocalDataSource();
        }

        #region Implementation of IResultsRepository

        public async Task<IResponseWrapper> GetTrainings(int idStudent)
        {
            try
            {
                var response = await _localDataSource.GetTrainings(idStudent);
                var list = TrainingMapper.ConvertFromListDto(response).ToList();
                return new Success<List<TrainingObservable>>(list);
            }
            catch (Exception e)
            {
                Debug.WriteLine("[ResultsRepository.GetTrainings()] Error: " + e.Message);
                return new Error(e.Message);
            }
        }

        #endregion
    }
}
