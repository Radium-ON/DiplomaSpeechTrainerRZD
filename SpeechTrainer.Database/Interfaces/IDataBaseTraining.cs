using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.Database.Interfaces
{
    public interface IDataBaseTraining<T, TV> : IDatabase<T, TV>
    {
        Task<List<T>> GetTrainingsByStudentAsync(int idStudent, bool enableSubQuery = false);
        Task<TV> CreateAsync(int idStudent, SituationDto situation, PositionDto position, T newObject);
    }
}
