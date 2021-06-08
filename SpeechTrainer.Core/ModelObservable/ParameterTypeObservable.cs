using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SpeechTrainer.Core.ModelObservable
{
    public class ParameterTypeObservable : ObservableValidator
    {
        public int Id { get; }
        public string TypeName { get; }

        public ParameterTypeObservable(int id, string typeName)
        {
            Id = id;
            TypeName = typeName;
        }
    }
}
