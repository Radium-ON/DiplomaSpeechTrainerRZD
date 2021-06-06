namespace SpeechTrainer.Database.Entities
{
    public class PositionDto
    {
        public int Id { get; }
        public string ShortName { get; }
        public string FullPosition { get; }
        public string Responsibilities { get; }

        public PositionDto() { }

        public PositionDto(string shortName, string fullPosition, string responsibilities)
        {
            ShortName = shortName;
            FullPosition = fullPosition;
            Responsibilities = responsibilities;
        }
        
        public PositionDto(int id, string shortName, string fullPosition, string responsibilities): this(shortName, fullPosition, responsibilities)
        {
            Id = id;
        }


        public override string ToString()
        {
            return ShortName + " " + FullPosition + " " + Responsibilities;
        }
    }
}
