using System.Threading.Tasks;

namespace SpeechTrainer.Database.Interfaces
{
    public interface IDataBaseGroup<T, TV> : IDatabase<T, TV>
    {
        Task<TV> CreateAsync(T newObject);
        Task<T> GetGroupByStudentAsync(int idStudent);
    }
}
