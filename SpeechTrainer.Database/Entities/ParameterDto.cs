using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTrainer.Database.Entities
{
    public class ParameterDto
    {
        public int Id { get; private set; }
        public int OrderNum { get; private set; }
        public AnswerFormDto AnswerForm { get; private set; }
        public AvailableValueDto AvailableValue { get; private set; }

        public ParameterDto(int orderNum, AvailableValueDto value, AnswerFormDto form) : this(orderNum, value)
        {
            AnswerForm = form;
        }

        public ParameterDto(int id, int orderNum, AvailableValueDto value, AnswerFormDto form) : this(orderNum, value, form)
        {
            Id = id;
        }

        public ParameterDto(int orderNum, AvailableValueDto value)
        {
            OrderNum = orderNum;
            AvailableValue = value;
        }

        public void SetAnswerForm(AnswerFormDto form)
        {
            AnswerForm = form;
        }

        public void SetAvailableValue(AvailableValueDto value)
        {
            AvailableValue = value;
        }

        public override string ToString()
        {
            return OrderNum + " " + AvailableValue;
        }
    }
}