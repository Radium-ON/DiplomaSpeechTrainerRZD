using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Database
{
    public interface IDataBaseAnswerForm<T, TV> : IDatabase<T, TV>
    {
        Task<List<T>> GetFormsByPositionAsync(int idPosition);
        Task<List<T>> GetFormsBySituationAsync(int idSituation);
        Task<TV> CreateAsync(int idParticipant, T newObject);
    }
}
