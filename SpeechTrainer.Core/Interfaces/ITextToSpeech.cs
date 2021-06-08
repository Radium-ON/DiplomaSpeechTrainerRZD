using System.Threading.Tasks;

namespace SpeechTrainer.Core.Interfaces
{
    public interface ITextToSpeech<T>
    {
        Task<T> GenerateSpeechToStreamAsync(string text);
        Task GenerateSpeechToSpeakersAsync(string text);
        Task GenerateSpeechToFileAsync(string text, string filePath);
    }
}