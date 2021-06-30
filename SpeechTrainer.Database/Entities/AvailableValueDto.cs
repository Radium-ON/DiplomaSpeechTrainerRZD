namespace SpeechTrainer.Database.Entities
{
    public class AvailableValueDto
    {
        public int Id { get; }
        public string Value { get; }
        public ParameterTypeDto ParameterType { get; private set; }

        public AvailableValueDto(int id, string value, ParameterTypeDto parmType) : this(id, value)
        {
            ParameterType = parmType;
        }

        public AvailableValueDto(int id, string value)
        {
            Id = id;
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
