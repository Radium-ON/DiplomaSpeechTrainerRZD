using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Interfaces
{
    public interface IDataBasePosition<T, TV> : IDatabase<T, TV>
    {
        Task<List<T>> GetPositionsBySituationAsync(int idSituation);
        Task<T> GetPositionByFormAsync(int idForm);
        Task<T> GetPositionByTrainingAsync(int idTraining);
        Task<TV> CreateAsync(T newObject);
    }
}
