using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using SpeechTrainer.Core.Interfaces;

namespace SpeechTrainer.Core.Utills
{
    public class SpeechService : ISpeechToText<RecognitionResult>, ITextToSpeech<SpeechSynthesisResult>
    {
        private readonly SpeechConfig _speechConfig;
        private readonly AudioConfig _audioConfig;
        private readonly SpeechRecognizer _speechRecognizer;
        private readonly SpeechSynthesizer _speechSynthesizer;
        private readonly IPlayer _mediaPlayer;

        public event EventHandler<SessionEventArgs> RecognizerSessionStarted;
        public event EventHandler<SessionEventArgs> RecognizerSessionStopped;
        public event EventHandler<SpeechRecognitionEventArgs> RecognizerRecognized;
        public event EventHandler<SpeechRecognitionEventArgs> RecognizerRecognizing;

        public event EventHandler<SpeechSynthesisEventArgs> SynthesizerSynthesisStarted;
        public event EventHandler<SpeechSynthesisEventArgs> SynthesizerSynthesisCompleted;
        public event EventHandler MediaPlaybackEnded;

        public SpeechService(IPlayer mediaPlayer)
        {
            _mediaPlayer = mediaPlayer;
            _mediaPlayer.PlaybackEnded += MediaPlayerOnPlaybackEnded;
            _speechConfig = SpeechConfig.FromSubscription("88ef55a3ae9a4d8c9c121ce17ae4db51", "northeurope");
            _speechConfig.SpeechRecognitionLanguage = "ru-ru";
            _speechConfig.EndpointId = "a7980ff0-7342-4299-9fe3-9a876e2135bc";
            _audioConfig = AudioConfig.FromDefaultMicrophoneInput();

            _speechRecognizer = new SpeechRecognizer(_speechConfig, _audioConfig);
            SetRecognizerEvents();
            var synthConfig = SpeechConfig.FromSubscription("88ef55a3ae9a4d8c9c121ce17ae4db51", "northeurope");
            synthConfig.SpeechSynthesisLanguage = "ru-ru";
            synthConfig.SpeechSynthesisVoiceName = "ru-RU-DmitryNeural";
            _speechSynthesizer = new SpeechSynthesizer(synthConfig);
            SetSynthesizerEvents();
        }

        private void MediaPlayerOnPlaybackEnded(object sender, EventArgs e)
        {
            MediaPlaybackEnded?.Invoke(sender, e);
        }

        private void SetSynthesizerEvents()
        {
            _speechSynthesizer.SynthesisStarted += (sender, synthesisEventArgs) =>
            {
                Debug.WriteLine("Синтез речи начат");
                SynthesizerSynthesisStarted?.Invoke(sender, synthesisEventArgs);
            };
            _speechSynthesizer.SynthesisCompleted += (sender, synthesisEventArgs) =>
            {
                Debug.WriteLine("Синтез речи завершен");
                SynthesizerSynthesisCompleted?.Invoke(sender, synthesisEventArgs);
            };
            _speechSynthesizer.Synthesizing += (sender, args) => { Debug.WriteLine("Синтез..."); };
        }

        private void SetRecognizerEvents()
        {
            _speechRecognizer.SessionStarted += (sender, sessionEventArgs) =>
            {
                Debug.WriteLine("Начало распознавания речи");
                RecognizerSessionStarted?.Invoke(sender, sessionEventArgs);
            };
            _speechRecognizer.SessionStopped += (sender, sessionEventArgs) =>
            {
                Debug.WriteLine("Распознавание речи окончено");
                RecognizerSessionStopped?.Invoke(sender, sessionEventArgs);
            };
            _speechRecognizer.Recognized += (sender, recognitionEventArgs) =>
            {
                Debug.WriteLine("Текст из речи получен");
                RecognizerRecognized?.Invoke(sender, recognitionEventArgs);
            };
            _speechRecognizer.Recognizing += (sender, recognitionEventArgs) =>
            {
                Debug.WriteLine("Распознавание в процессе:");
                Debug.Write($"{recognitionEventArgs.Result.Text}");
                RecognizerRecognizing?.Invoke(sender, recognitionEventArgs);
            };
        }

        #region Implementation of ISpeechToText<RecognitionResult>

        public async Task<RecognitionResult> RecognizeSpeechFromMicrophoneAsync()
        {
            return await _speechRecognizer.RecognizeOnceAsync();
        }

        #endregion

        #region Implementation of ITextToSpeech<SpeechSynthesisResult>

        public async Task<SpeechSynthesisResult> GenerateSpeechToStreamAsync(string text)
        {
            throw new NotImplementedException();
        }

        public async Task GenerateSpeechToSpeakersAsync(string text)
        {
            // Receive a text from "Text for Synthesizing" text box and synthesize it to speaker.
            var result = await _speechSynthesizer.SpeakTextAsync(text)
                .ConfigureAwait(false);
            // Checks result.
            if (result.Reason == ResultReason.SynthesizingAudioCompleted)
            {
                //NotifyUser($"Speech Synthesis Succeeded.", NotifyType.StatusMessage);

                // Since native playback is not yet supported on UWP yet (currently only supported on Windows/Linux Desktop),
                // use the WinRT API to play audio here as a short term solution
                using var audioStream = AudioDataStream.FromResult(result);
                // Save synthesized audio data as a wave file and user MediaPlayer to play it
                var filePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "output_audio_for_playback.wav");
                await audioStream.SaveToWaveFileAsync(filePath);
                await _mediaPlayer.PlayFromFile(filePath);

            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"CANCELED: Reason={cancellation.Reason}");
                sb.AppendLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                sb.AppendLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");

                //NotifyUser(sb.ToString(), NotifyType.ErrorMessage);
                Debug.WriteLine(sb);
            }

        }

        public async Task GenerateSpeechToFileAsync(string text, string filePath)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void StopOperations()
        {
            _speechSynthesizer.StopSpeakingAsync();
            _mediaPlayer.StopAudio();
        }
    }
}
