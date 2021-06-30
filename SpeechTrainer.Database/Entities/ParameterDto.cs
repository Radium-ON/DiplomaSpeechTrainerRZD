namespace SpeechTrainer.Database.Entities
{
    public class ParameterDto
    {
        public int Id { get; }
        public int OrderNum { get; }
        public int AnswerFormId { get; }
        public AvailableValueDto Value { get; private set; }

        public ParameterDto(int orderNum, AvailableValueDto value, int formId) : this(orderNum, value)
        {
            AnswerFormId = formId;
        }

        public ParameterDto(int id, int orderNum, AvailableValueDto value, int form) : this(orderNum, value, form)
        {
            Id = id;
        }

        public ParameterDto(int orderNum, AvailableValueDto value)
        {
            OrderNum = orderNum;
            Value = value;
        }

        public void SetAvailableValue(AvailableValueDto value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return OrderNum + " " + Value;
        }
    }
}