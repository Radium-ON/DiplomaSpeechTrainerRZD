namespace SpeechTrainer.Database.Entityes
{
    public class TypeDTO
    {
        public int Id { get; private set; }
        public string Title { get; private set; }

        public TypeDTO(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public TypeDTO(string title)
        {
            Title = title;
        }

        public TypeDTO() {}
    }
}
