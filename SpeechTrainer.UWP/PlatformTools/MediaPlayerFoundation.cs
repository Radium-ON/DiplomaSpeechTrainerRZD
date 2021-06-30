using System;
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

        public MediaPlayerFoundation()
        {
            _mediaPlayer.MediaEnded += OnPlaybackEnded;
        }
        public async Task PlayFromFile(string filePath)
        {
            _mediaPlayer.Source =
                MediaSource.CreateFromStorageFile(await StorageFile.GetFileFromPathAsync(filePath));
            _mediaPlayer.PlaybackRate = 1.2;
            _mediaPlayer.Play();
        }

        public event EventHandler PlaybackEnded;

        public void StopAudio()
        {
            _mediaPlayer.Pause();
        }

        protected virtual void OnPlaybackEnded(MediaPlayer sender, object args)
        {
            PlaybackEnded?.Invoke(sender, (EventArgs)args);
        }
    }
}
