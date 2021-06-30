using System.Threading.Tasks;

namespace SpeechTrainer.Database.Interfaces
{
    public interface IDataBaseParameterType<T, TV> : IDatabase<T, TV>
    {
        Task<TV> CreateAsync(T newObject);
    }
}
