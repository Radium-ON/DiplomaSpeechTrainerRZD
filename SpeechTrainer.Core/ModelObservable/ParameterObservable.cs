using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SpeechTrainer.Core.ModelObservable
{
    public class ParameterObservable : ObservableValidator
    {
        public int Id { get; }
        public int OrderNum { get; }
        public int AnswerFormId { get; }
        public AvailableValueObservable Value { get; private set; }

        public ParameterObservable(int id, int orderNum, int answerFormId, AvailableValueObservable value)
        {
            Id = id;
            OrderNum = orderNum;
            AnswerFormId = answerFormId;
            Value = value;
        }
    }
}