using System.Threading.Tasks;

namespace SpeechTrainer.Database.Interfaces
{
    public interface IDataBaseStudent<T, TV> : IDatabase<T, TV>
    {
        Task<TV> CreateAsync(T newObject);
    }
}
