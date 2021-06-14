using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.UWP.Training.TrainingStart.Data
{
    interface ITrainingStartDataSource
    {
        Task<List<SituationDto>> GetSituations();
    }
}
