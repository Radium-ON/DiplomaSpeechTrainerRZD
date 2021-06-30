using System.Collections.Generic;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SpeechTrainer.Core.ModelObservable
{
    public class AnswerFormObservable : ObservableValidator
    {
        public int Id { get; }
        public int OrderNum { get; }
        public PhraseObservable Phrase { get; private set; }
        public SituationObservable Situation { get; private set; }
        public PositionObservable Position { get; private set; }
        public List<ParameterObservable> Parameters { get; private set; }

        public AnswerFormObservable(int id, int orderNum, PhraseObservable phrase, SituationObservable situation, PositionObservable position, List<ParameterObservable> parameters)
        {
            Id = id;
            OrderNum = orderNum;
            Phrase = phrase;
            Situation = situation;
            Position = position;
            Parameters = parameters;
        }
    }
}
