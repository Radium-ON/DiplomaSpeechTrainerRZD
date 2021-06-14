using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Database;
using SpeechTrainer.Database.Entities;
using SpeechTrainer.Database.Interfaces;

namespace SpeechTrainer.UWP.Training.HistoryDetails.Data
{
    public class HistoryDetailsLocalDataSource : IHistoryDetailsDataSource
    {
        private readonly IDataBaseTrainingLine<TrainingLineDto, bool> _dbTrainingLine;
        private readonly IDataBasePosition<PositionDto, bool> _dbPosition;

        public HistoryDetailsLocalDataSource()
        {
            _dbTrainingLine = new DataBaseTrainingLine();
            _dbPosition = new DataBasePosition();
        }

        #region Implementation of IHistoryDetailsDataSource

        public async Task<List<TrainingLineDto>> GetTrainingLines(int idTraining)
        {
            return await _dbTrainingLine.GetLinesByTrainingAsync(idTraining);
        }

        public async Task<PositionDto> GetPosition(int idTraining)
        {
            return await _dbPosition.GetPositionByTrainingAsync(idTraining);
        }

        #endregion
    }
}
