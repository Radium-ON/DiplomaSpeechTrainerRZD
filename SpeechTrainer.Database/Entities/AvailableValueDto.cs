using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTrainer.Database.Entities
{
    public class AvailableValueDto
    {
        public int Id { get; private set; }
        public int ParmTypeId { get; private set; }
        public ParameterTypeDto ParameterType { get; private set; }
        public string Value { get; private set; }

        public AvailableValueDto(int id, int parmTypeIdId)
        {
            Id = id;
            ParmTypeId = parmTypeIdId;
        }

        public AvailableValueDto(int id, int parameterTypeId, string value) : this(parameterTypeId, value)
        {
            ParmTypeId = parameterTypeId;
            Value = value;
        }

        public AvailableValueDto(int parameterType, string value)
        {
            ParmTypeId = parameterType;
            Value = value;
        }

        public AvailableValueDto()
        {
        }

        public void SetParameterType(ParameterTypeDto parameterType)
        {
            ParameterType = parameterType;
        }
    }
}
