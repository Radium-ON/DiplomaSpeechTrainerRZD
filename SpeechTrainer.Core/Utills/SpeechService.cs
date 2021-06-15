using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public event EventHandler<SpeechRecognitionEventArgs> RecognizerRecognizing;

        public event EventHandler<SpeechSynthesisEventArgs> SynthesizerSynthesisStarted;
        public event EventHandler<SpeechSynthesisEventArgs> SynthesizerSynthesisCompleted;

        public SpeechService()
        {
            _speechConfig = SpeechConfig.FromSubscription("88ef55a3ae9a4d8c9c121ce17ae4db51", "northeurope");
            _speechConfig.SpeechRecognitionLanguage = "ru-ru";
            _speechConfig.SpeechSynthesisLanguage = "ru-ru";
            _speechConfig.SpeechSynthesisVoiceName = "ru-RU-DmitryNeural";
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
            await _speechSynthesizer.SpeakTextAsync(text);

        }

        public async Task GenerateSpeechToFileAsync(string text, string filePath)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
