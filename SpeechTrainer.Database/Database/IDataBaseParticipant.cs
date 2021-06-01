using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Database
{
    public interface IDataBaseParticipant<T, TV> : IDatabase<T, TV>
    {
        Task<List<T>> GetParticipantsBySituationAsync(string situationName);
        Task<List<T>> GetParticipantsByPositionAsync(int idPosition);
        Task<TV> CreateAsync(int idPosition, T newObject);
    }
}
