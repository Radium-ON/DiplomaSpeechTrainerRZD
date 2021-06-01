using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTrainer.Database.Entities
{
    public class AvailableValueDto
    {
        public int Id { get; private set; }
        public ParameterTypeDto ParameterType{ get; private set; }
        public string Value { get; private set; }

        public AvailableValueDto(int id, ParameterTypeDto parameterType, string value)
        {
            Id = id;
            ParameterType = parameterType;
            Value = value;
        }

        public void SetParameterType(ParameterTypeDto parameterType)
        {
            ParameterType = parameterType;
        }
    }
}
