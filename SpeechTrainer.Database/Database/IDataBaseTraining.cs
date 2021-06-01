using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Database
{
    public interface IDataBaseTraining<T, TV> : IDatabase<T, TV>
    {
        Task<List<T>> GetTrainingsByStudentAsync(int idStudent);
        Task<TV> CreateAsync(int idStudent, int idParticipant, T newObject);
    }
}
