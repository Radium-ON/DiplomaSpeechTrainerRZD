using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Database
{
    public interface IDataBaseParameterType<T, TV> : IDatabase<T, TV>
    {
        Task<TV> CreateAsync(T newObject);
    }
}
