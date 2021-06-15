using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using SpeechTrainer.Core.Interfaces;

namespace SpeechTrainer.UWP.PlatformTools
{
    public class MediaPlayerFoundation : IPlayer
    {
        private readonly MediaPlayer _mediaPlayer = new MediaPlayer();
        public async Task PlayFromFile(string filePath)
        {
            _mediaPlayer.Source =
                MediaSource.CreateFromStorageFile(await StorageFile.GetFileFromPathAsync(filePath));
            _mediaPlayer.PlaybackRate = 1.2;
            _mediaPlayer.Play();
        }

        public void StopAudio()
        {
            _mediaPlayer.Pause();
        }
    }
}
