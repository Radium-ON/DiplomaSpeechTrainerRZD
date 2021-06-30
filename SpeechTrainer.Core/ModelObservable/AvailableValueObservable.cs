using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SpeechTrainer.Core.ModelObservable
{
    public class AvailableValueObservable : ObservableValidator
    {
        public int Id { get; }
        public string Value { get; }
        public ParameterTypeObservable ParameterType { get; private set; }

        public AvailableValueObservable(int id, string value, ParameterTypeObservable parameterType)
        {
            Id = id;
            Value = value;
            ParameterType = parameterType;
        }
    }
}