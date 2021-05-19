using System.Threading.Tasks;

namespace SpeechTrainer.Database.Database
{
    public interface IDatabaseInterval<T, V> : IDatabase<T, V>
    {
        Task<V> InsertInterval(int idTask, T interval);
    }
}
