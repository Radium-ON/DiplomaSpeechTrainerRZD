using System.Threading.Tasks;

namespace SpeechTrainer.Core.Interfaces
{
    public interface IPlayer
    {
        Task PlayFromFile(string filePath);
        void StopAudio();
    }
}