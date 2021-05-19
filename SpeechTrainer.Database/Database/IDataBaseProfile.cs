using System.Threading.Tasks;

namespace SpeechTrainer.Database.Database
{
    public interface IDataBaseProfile<T, V> : IDatabase<T, V>
    {
        Task<V> Create(T newObject);
    }
}
