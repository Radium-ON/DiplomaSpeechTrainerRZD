using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Interfaces
{
    public interface IDataBaseSituation<T, TV> : IDatabase<T, TV>
    {
        Task<List<T>> GetSituationsByPositionAsync(int idPosition);
        Task<T> GetSituationByTrainingAsync(int idTraining);
        Task<TV> CreateAsync(int idPosition, T newObject);
    }
}
