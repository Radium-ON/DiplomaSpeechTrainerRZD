using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Database;
using SpeechTrainer.Database.Entities;
using SpeechTrainer.Database.Interfaces;

namespace SpeechTrainer.UWP.Training.TrainingStart.Data
{
    public class TrainingStartLocalDataSource : ITrainingStartDataSource
    {
        private readonly IDataBaseSituation<SituationDto, bool> _dbSituation;

        public TrainingStartLocalDataSource()
        {
            _dbSituation = new DataBaseSituation();
        }

        #region Implementation of ITrainingStartDataSource

        public async Task<List<SituationDto>> GetSituations()
        {
            return await _dbSituation.SelectAllAsync(true);
        }

        #endregion
    }
}
