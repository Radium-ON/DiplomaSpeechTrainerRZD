using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Database
{
    public interface IDataBaseAvailableValue<T, TV> : IDatabase<T, TV>
    {
        Task<List<T>> GetAvailableValuesByParameterTypeAsync(int idParameterType);
        Task<TV> CreateAsync(int idParameterType, T newObject);
    }
}
