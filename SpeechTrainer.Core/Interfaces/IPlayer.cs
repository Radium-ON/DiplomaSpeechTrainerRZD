using System;
using System.Threading.Tasks;

namespace SpeechTrainer.Core.Interfaces
{
    public interface IPlayer
    {
        event EventHandler PlaybackEnded;
        Task PlayFromFile(string filePath);
        void StopAudio();
    }
}