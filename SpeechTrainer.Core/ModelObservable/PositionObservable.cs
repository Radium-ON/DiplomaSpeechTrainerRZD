using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SpeechTrainer.Core.ModelObservable
{
    public class PositionObservable : ObservableValidator
    {
        public int Id { get; }
        public string ShortName { get; }
        public string FullPosition { get; }
        public string Responsibilities { get; }

        public PositionObservable(int id, string shortName, string fullPosition, string responsibilities)
        {
            Id = id;
            ShortName = shortName;
            FullPosition = fullPosition;
            Responsibilities = responsibilities;
        }
    }
}