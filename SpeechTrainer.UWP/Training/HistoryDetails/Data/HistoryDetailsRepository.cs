using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SpeechTrainer.Core.DtoMappers;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.UWP.Training.History.Data;

namespace SpeechTrainer.UWP.Training.HistoryDetails.Data
{
    public class HistoryDetailsRepository : IHistoryDetailsRepository
    {
        private readonly IHistoryDetailsDataSource _localDataSource;

        public HistoryDetailsRepository()
        {
            _localDataSource = new HistoryDetailsLocalDataSource();
        }

        #region Implementation of IHistoryDetailsRepository

        public async Task<IResponseWrapper> GetTrainingLines(int idTraining)
        {
            try
            {
                var response = await _localDataSource.GetTrainingLines(idTraining);
                var list = TrainingLineMapper.ConvertFromListDto(response).ToList();
                return new Success<List<TrainingLineObservable>>(list);
            }
            catch (Exception e)
            {
                Debug.WriteLine("[HistoryDetailsRepository.GetSituations()] Error: " + e.Message);
                return new Error(e.Message);
            }
        }

        public async Task<IResponseWrapper> GetPosition(int idTraining)
        {
            try
            {
                var response = await _localDataSource.GetPosition(idTraining);
                var position = PositionMapper.ConvertFromDto(response);
                return new Success<PositionObservable>(position);
            }
            catch (Exception e)
            {
                Debug.WriteLine("[HistoryDetailsRepository.GetPosition()] Error: " + e.Message);
                return new Error(e.Message);
            }
        }

        #endregion
    }
}
