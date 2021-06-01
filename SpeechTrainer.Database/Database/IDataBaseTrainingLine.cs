using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Database
{
    public interface IDataBaseTrainingLine<T, TV> : IDatabase<T, TV>
    {
        Task<List<T>> GetLinesByTrainingAsync(int idTraining);
        Task<TV> CreateAsync(int idTraining, T newObject);
    }
}
