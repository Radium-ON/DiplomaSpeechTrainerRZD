using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.UWP.User.Results.Data
{
    interface IResultsDataSource
    {
        Task<List<TrainingDto>> GetTrainings(int idStudent);
    }
}
