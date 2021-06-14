using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.UWP.Training.History.Data
{
    interface ITrainingDataSource
    {
        Task<List<TrainingDto>> GetAllTrainings();
        Task<List<TrainingDto>> GetTrainings(int idStudent);
    }
}
