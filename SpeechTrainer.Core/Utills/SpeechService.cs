using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public event EventHandler<SessionEventArgs> RecognizerSessionStarted;
        public event EventHandler<SessionEventArgs> RecognizerSessionStopped;
        public event EventHandler<SpeechRecognitionEventArgs> RecognizerRecognized;

        public event EventHandler<SpeechSynthesisEventArgs> SynthesizerSynthesisStarted;
        public event EventHandler<SpeechSynthesisEventArgs> SynthesizerSynthesisCompleted;

        public SpeechService()
        {
            _speechConfig = SpeechConfig.FromSubscription("88ef55a3ae9a4d8c9c121ce17ae4db51", "northeurope");
            _speechConfig.SpeechRecognitionLanguage = "ru-ru";
            _audioConfig = AudioConfig.FromDefaultMicrophoneInput();

            _speechRecognizer = new SpeechRecognizer(_speechConfig, _audioConfig);
            SetRecognizerEvents();
            _speechSynthesizer = new SpeechSynthesizer(_speechConfig);
            SetSynthesizerEvents();
        }

        private void SetSynthesizerEvents()
        {
            _speechSynthesizer.SynthesisStarted += (sender, synthesisEventArgs) =>
            {
                SynthesizerSynthesisStarted?.Invoke(sender, synthesisEventArgs);
            };
            _speechSynthesizer.SynthesisCompleted += (sender, synthesisEventArgs) =>
            {
                SynthesizerSynthesisCompleted?.Invoke(sender, synthesisEventArgs);
            };
        }

        private void SetRecognizerEvents()
        {
            _speechRecognizer.SessionStarted += (sender, sessionEventArgs) =>
            {
                RecognizerSessionStarted?.Invoke(sender, sessionEventArgs);
            };
            _speechRecognizer.SessionStopped += (sender, sessionEventArgs) =>
            {
                RecognizerSessionStopped?.Invoke(sender, sessionEventArgs);
            };
            _speechRecognizer.Recognized += (sender, recognitionEventArgs) =>
            {
                RecognizerRecognized?.Invoke(sender, recognitionEventArgs);
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
            await _speechSynthesizer.SpeakTextAsync(text);

        }

        public async Task GenerateSpeechToFileAsync(string text, string filePath)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
