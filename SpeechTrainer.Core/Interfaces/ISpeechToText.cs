using System.Threading.Tasks;

namespace SpeechTrainer.Core.Interfaces
{
    public interface ISpeechToText<T>
    {
        Task<T> RecognizeSpeechFromMicrophoneAsync();
    }
}