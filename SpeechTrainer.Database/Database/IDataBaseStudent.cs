using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Database
{
    public interface IDataBaseStudent<T, TV> : IDatabase<T, TV>
    {
        Task<TV> Create(T newObject);
    }
}
