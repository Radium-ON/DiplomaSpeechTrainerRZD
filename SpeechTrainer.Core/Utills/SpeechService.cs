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

        public SpeechService()
        {
            _speechConfig = SpeechConfig.FromSubscription("88ef55a3ae9a4d8c9c121ce17ae4db51", "northeurope");
            _speechConfig.SpeechRecognitionLanguage = "ru-ru";
            _audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        }

        #region Implementation of ISpeechToText<RecognitionResult>

        public async Task<RecognitionResult> RecognizeSpeechFromMicrophoneAsync()
        {
            using var recognizer = new SpeechRecognizer(_speechConfig, _audioConfig);

            return await recognizer.RecognizeOnceAsync();
        }

        #endregion

        #region Implementation of ITextToSpeech<SpeechSynthesisResult>

        public async Task<SpeechSynthesisResult> GenerateSpeechToStreamAsync(string text)
        {
            throw new NotImplementedException();
        }

        public async Task GenerateSpeechToSpeakersAsync(string text)
        {
            using var synthesizer = new SpeechSynthesizer(_speechConfig);
            await synthesizer.SpeakTextAsync(text);
        }

        public async Task GenerateSpeechToFileAsync(string text, string filePath)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
