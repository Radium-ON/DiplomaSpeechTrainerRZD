using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTrainer.Database.Entities
{
    public class ParameterDto
    {
        public int Id { get; private set; }
        public int OrderNum { get; private set; }
        public string Value { get; private set; }
        public AnswerFormDto AnswerForm { get; private set; }
        public ParameterTypeDto ParameterType { get; private set; }

        public ParameterDto(int orderNum, string value, AnswerFormDto form) : this(orderNum, value)
        {
            AnswerForm = form;
        }

        public ParameterDto(int id, int orderNum, string value, AnswerFormDto form) : this(orderNum, value, form)
        {
            Id = id;
        }

        public ParameterDto(int orderNum, string value)
        {
            OrderNum = orderNum;
            Value = value;
        }

        public ParameterDto(int id, int orderNum, string value, AnswerFormDto form, ParameterTypeDto parameterType) : this(id, orderNum, value, form)
        {
            ParameterType = parameterType;
        }

        public void SetAnswerForm(AnswerFormDto form)
        {
            AnswerForm = form;
        }

        public void SetParameterType(ParameterTypeDto type)
        {
            ParameterType = type;
        }

        public override string ToString()
        {
            return OrderNum + " " + Value;
        }
    }
}