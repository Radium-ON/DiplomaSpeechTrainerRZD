using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Database
{
    public interface IDataBaseAnswerForm<T, TV> : IDatabase<T, TV>
    {
        Task<List<T>> GetFormsByParticipantAsync(int idParticipant);
        Task<List<T>> GetFormsBySituationAsync(string situationName);
        Task<TV> CreateAsync(int idParticipant, T newObject);
    }
}
