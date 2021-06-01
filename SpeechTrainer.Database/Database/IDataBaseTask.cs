using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Database
{
    public interface IDataBaseTask<T, TV> : IDatabase<T, TV>
    {
        Task<List<T>> GetTaskByProfile(int idProfile);
        Task<TV> Create(int idProfile, T newObject);

        Task<TV> CloseTask(int idTask, bool isClosed);

    }
}
