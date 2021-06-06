using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.Database.Interfaces
{
    public interface IDataBaseTraining<T, TV> : IDatabase<T, TV>
    {
        Task<List<T>> GetTrainingsByStudentAsync(int idStudent);
        Task<TV> CreateAsync(StudentDto student, SituationDto situation, PositionDto position, T newObject);
    }
}
