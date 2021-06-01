using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Database
{
    public interface IDatabase<T, TV>
    {
        Task<List<T>> SelectAllAsync();
        Task<T> SelectByIdAsync(int id);
        Task<TV> UpdateAsync(T newObject);
        Task<TV> DeleteAsync(int id);
    }
}
