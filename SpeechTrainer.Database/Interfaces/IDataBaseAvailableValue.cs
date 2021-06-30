using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Interfaces
{
    public interface IDataBaseAvailableValue<T, TV> : IDatabase<T, TV>
    {
        Task<List<T>> GetAvailableValuesByParameterTypeAsync(int idParameterType);
        Task<T> GetAvailableValueByParameterAsync(int idParameter);
        Task<TV> CreateAsync(int idParameterType, T newObject);
    }
}
