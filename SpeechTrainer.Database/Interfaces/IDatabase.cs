using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Interfaces
{
    public interface IDatabase<T, TV>
    {
        Task<List<T>> SelectAllAsync(bool includeNestedData = false);
        Task<T> SelectByIdAsync(int id, bool includeNestedData = false);
        Task<TV> UpdateAsync(T newObject);
        Task<TV> DeleteAsync(int id);
    }
}
