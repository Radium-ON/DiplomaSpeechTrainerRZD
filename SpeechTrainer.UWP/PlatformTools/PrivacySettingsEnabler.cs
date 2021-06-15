using System;
using System.Threading.Tasks;
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
                var mediaCapture = new Windows.Media.Capture.MediaCapture();
                var settings =
                    new Windows.Media.Capture.MediaCaptureInitializationSettings
                    {
                        StreamingCaptureMode = Windows.Media.Capture.StreamingCaptureMode.Audio
                    };
                await mediaCapture.InitializeAsync(settings);
            }
            catch (Exception)
            {
                isMicAvailable = false;
            }

            if (!isMicAvailable)
            {
                await Windows.System.Launcher.LaunchUriAsync(
                    new Uri("ms-settings:privacy-microphone"));
            }
        }

        #endregion
    }
}
