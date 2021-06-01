using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechTrainer.Database.Database
{
    public interface IDataBaseParameter<T, TV> : IDatabase<T, TV>
    {
        Task<List<T>> GetParametersByAnswerFormAsync(int idForm);
        Task<TV> CreateAsync(int idForm, int idParameterType, T newObject);
    }
}
