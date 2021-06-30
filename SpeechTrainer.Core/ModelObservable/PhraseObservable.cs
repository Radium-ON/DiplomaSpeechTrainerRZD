using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SpeechTrainer.Core.ModelObservable
{
    public class PhraseObservable : ObservableValidator
    {
        public int Id { get; }
        public string Text { get; }

        public PhraseObservable(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}