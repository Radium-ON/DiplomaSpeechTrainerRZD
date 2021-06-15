using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SpeechTrainer.Core.DtoMappers;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.ResponseWrapper;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.UWP.Training.TrainingRun.Data
{
    public class TrainingRunRepository : ITrainingRunRepository
    {
        private readonly ITrainingRunDataSource _localDataSource;

        public TrainingRunRepository()
        {
            _localDataSource = new TrainingRunLocalDataSource();
        }

        #region Implementation of ITrainingRunRepository

        public async Task<IResponseWrapper> GetForms(int idSituation)
        {
            try
            {
                var response = await _localDataSource.GetForms(idSituation);
                var list = AnswerFormMapper.ConvertFromListDto(response).ToList();
                return new Success<List<AnswerFormObservable>>(list);
            }
            catch (Exception e)
            {
                Debug.WriteLine("[TrainingRunRepository.GetForms()] Error: " + e.Message);
                return new Error(e.Message);
            }
        }

        public async Task<IResponseWrapper> CreateTraining(int studentId, SituationObservable situation, PositionObservable position, TrainingObservable newObject)
        {
            try
            {
                var response = await _localDataSource.CreateTraining(
                    studentId,
                    SituationMapper.ConvertToDto(situation),
                    PositionMapper.ConvertToDto(position),
                    TrainingMapper.ConvertToDto(newObject));
                return new Success<bool>(response);
            }
            catch (Exception e)
            {
                Debug.WriteLine("[TrainingRunRepository.CreateTraining()] Error: " + e.Message);
                return new Error(e.Message);
            }
        }

        #endregion
    }
}
