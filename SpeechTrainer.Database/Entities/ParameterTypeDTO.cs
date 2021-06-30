namespace SpeechTrainer.Database.Entities
{
    public class ParameterTypeDto
    {
        public int Id { get; }
        public string TypeName { get; }

        public ParameterTypeDto()
        {
            
        }

        public ParameterTypeDto(int id, string typeName)
        {
            Id = id;
            TypeName = typeName;
        }
    }
}
