namespace SpeechTrainer.Database.Entities
{
    public class PhraseDto
    {
        public int Id { get; }
        public string Text { get; }

        public PhraseDto()
        {
            
        }

        public PhraseDto(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}
