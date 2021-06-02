using System.Threading.Tasks;

namespace SpeechTrainer.Database.Interfaces
{
    public interface IDataBaseProfile<T, V> : IDatabase<T, V>
    {
        Task<V> Create(T newObject);
    }
}
