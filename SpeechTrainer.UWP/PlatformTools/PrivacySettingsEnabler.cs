using System;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.System;
using SpeechTrainer.Core.Interfaces;

namespace SpeechTrainer.UWP.PlatformTools
{
    public class PrivacySettingsEnabler : IPrivacySettings
    {
        #region Implementation of IPrivacySettings

        public async Task EnableMicrophoneAsync()
        {
            var isMicAvailable = true;
            try
            {
                var mediaCapture = new MediaCapture();
                var settings =
                    new MediaCaptureInitializationSettings
                    {
                        StreamingCaptureMode = StreamingCaptureMode.Audio
                    };
                await mediaCapture.InitializeAsync(settings);
            }
            catch (Exception)
            {
                isMicAvailable = false;
            }

            if (!isMicAvailable)
            {
                await Launcher.LaunchUriAsync(
                    new Uri("ms-settings:privacy-microphone"));
            }
        }

        #endregion
    }
}
