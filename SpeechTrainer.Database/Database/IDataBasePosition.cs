using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Database
{
    public interface IDataBasePosition<T, TV> : IDatabase<T, TV>
    {
        Task<TV> CreateAsync(T newObject);
    }
}
