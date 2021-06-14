using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SpeechTrainer.Core.DtoMappers;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;

namespace SpeechTrainer.UWP.Training.History.Data
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly ITrainingDataSource _localDataSource;

        public TrainingRepository()
        {
            _localDataSource = new TrainingLocalDataSource();
        }

        #region Implementation of ITrainingRepository

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
                Debug.WriteLine("[TrainingRepository.GetTrainings()] Error: " + e.Message);
                return new Error(e.Message);
            }
        }

        #endregion
    }
}
