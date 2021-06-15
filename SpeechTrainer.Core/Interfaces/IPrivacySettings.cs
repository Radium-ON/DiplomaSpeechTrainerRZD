using System.Threading.Tasks;

namespace SpeechTrainer.Core.Interfaces
{
    public interface IPrivacySettings
    {
        Task EnableMicrophoneAsync();
    }
}