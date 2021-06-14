using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SpeechTrainer.Core.DtoMappers;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;

namespace SpeechTrainer.UWP.Training.TrainingStart.Data
{
    public class TrainingStartRepository : ITrainingStartRepository
    {
        private readonly ITrainingStartDataSource _localDataSource;

        public TrainingStartRepository()
        {
            _localDataSource = new TrainingStartLocalDataSource();
        }

        #region Implementation of ITrainingStartRepository

        public async Task<IResponseWrapper> GetSituations()
        {
            try
            {
                var response = await _localDataSource.GetSituations();
                var list = SituationMapper.ConvertFromListDto(response).ToList();
                return new Success<List<SituationObservable>>(list);
            }
            catch (Exception e)
            {
                Debug.WriteLine("[TrainingStartRepository.GetSituations()] Error: " + e.Message);
                return new Error(e.Message);
            }
        }

        #endregion
    }
}
