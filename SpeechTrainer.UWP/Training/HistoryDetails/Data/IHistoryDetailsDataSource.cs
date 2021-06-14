using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.UWP.Training.HistoryDetails.Data
{
    interface IHistoryDetailsDataSource
    {
        Task<List<TrainingLineDto>> GetTrainingLines(int idTraining);
        Task<PositionDto> GetPosition(int idTraining);
    }
}
