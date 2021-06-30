using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Database;
using SpeechTrainer.Database.Entities;
using SpeechTrainer.Database.Interfaces;

namespace SpeechTrainer.UWP.Training.History.Data
{
    public class TrainingLocalDataSource : ITrainingDataSource
    {
        private readonly IDataBaseTraining<TrainingDto, bool> _dbTraining;

        public TrainingLocalDataSource()
        {
            _dbTraining = new DataBaseTraining();
        }

        #region Implementation of ITrainingDataSource

        public async Task<List<TrainingDto>> GetAllTrainings()
        {
            return await _dbTraining.SelectAllAsync();
        }

        public async Task<List<TrainingDto>> GetTrainings(int idStudent)
        {
            return await _dbTraining.GetTrainingsByStudentAsync(idStudent);
        }

        #endregion
    }
}
