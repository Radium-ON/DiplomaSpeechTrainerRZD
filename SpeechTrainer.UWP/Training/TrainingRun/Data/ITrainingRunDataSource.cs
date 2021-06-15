using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.UWP.Training.TrainingRun.Data
{
    interface ITrainingRunDataSource
    {
        Task<List<AnswerFormDto>> GetForms(int idSituation);
        Task<bool> CreateTraining(int idStudent, SituationDto situation, PositionDto position, TrainingDto newObject);
    }
}
