using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Database
{
    public interface IDatabase<T, V>
    {
        Task<List<T>> SelectAll();
        Task<T> SelectById(int id);
        Task<V> Update(T newObject);
        Task<V> Delete(int id);
    }
}
