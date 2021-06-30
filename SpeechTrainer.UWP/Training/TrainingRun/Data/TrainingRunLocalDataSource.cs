using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Database;
using SpeechTrainer.Database.Entities;
using SpeechTrainer.Database.Interfaces;

namespace SpeechTrainer.UWP.Training.TrainingRun.Data
{
    public class TrainingRunLocalDataSource : ITrainingRunDataSource
    {
        private readonly IDataBaseAnswerForm<AnswerFormDto, bool> _dbAnswerForm;
        private readonly IDataBaseTraining<TrainingDto, bool> _dbTraining;

        public TrainingRunLocalDataSource()
        {
            _dbAnswerForm = new DataBaseAnswerForm();
            _dbTraining = new DataBaseTraining();
        }

        #region Implementation of ITrainingRunDataSource

        public async Task<List<AnswerFormDto>> GetForms(int idSituation)
        {
            return await _dbAnswerForm.GetFormsBySituationAsync(idSituation);
        }

        public async Task<bool> CreateTraining(int idStudent, SituationDto situation, PositionDto position, TrainingDto newObject)
        {
            return await _dbTraining.CreateAsync(idStudent, situation, position, newObject);
        }

        #endregion
    }
}
