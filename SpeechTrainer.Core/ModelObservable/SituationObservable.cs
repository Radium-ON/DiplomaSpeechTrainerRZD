using System.Collections.Generic;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SpeechTrainer.Core.ModelObservable
{
    public class SituationObservable : ObservableValidator
    {
        public int Id { get; }
        public List<PositionObservable> Positions { get; private set; }
        public List<AnswerFormObservable> AnswerForms { get; private set; }
        public string Name { get; }
        public string Description { get; }

        public SituationObservable(int id, List<PositionObservable> positions, List<AnswerFormObservable> answerForms, string name, string description)
        {
            Id = id;
            Positions = positions;
            AnswerForms = answerForms;
            Name = name;
            Description = description;
        }
    }
}