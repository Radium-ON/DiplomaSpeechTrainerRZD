using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Database;
using SpeechTrainer.Database.Entities;
using SpeechTrainer.Database.Interfaces;
using SpeechTrainer.UWP.Training.History.Data;

namespace SpeechTrainer.UWP.User.Results.Data
{
    public class ResultsLocalDataSource : IResultsDataSource
    {
        private readonly IDataBaseTraining<TrainingDto, bool> _dbTraining;

        public ResultsLocalDataSource()
        {
            _dbTraining = new DataBaseTraining();
        }

        #region Implementation of IResultsDataSource

        public async Task<List<TrainingDto>> GetTrainings(int idStudent)
        {
            return await _dbTraining.GetTrainingsByStudentAsync(idStudent, true);
        }

        #endregion
    }
}
