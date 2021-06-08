
namespace SpeechTrainer.Core.Utills
{
    public enum LengthText
    {
        TypeLength = 20,
        TitleLength = 200,
        NameLength = 100,
        SubtitleLength = 150,
        DescriptionLength = 500,
    }

    public static class Validator
    {
        private const int TypeLength = 20;
        private const int TitleLength = 200;
        private const int NameLength = 100;
        private const int SubtitleLength = 150;
        private const int DescriptionLength = 500;

        public static bool ValidateTextField(string text, LengthText count)
        {
            text = text.Trim();
            return count switch
            {
                LengthText.DescriptionLength => (text.Length <= DescriptionLength && text.Length > 0),
                LengthText.NameLength => (text.Length <= NameLength && text.Length > 0),
                LengthText.SubtitleLength => (text.Length <= SubtitleLength && text.Length > 0),
                LengthText.TitleLength => (text.Length <= TitleLength && text.Length > 0),
                LengthText.TypeLength => (text.Length <= TypeLength && text.Length > 0),
                _ => (text.Length > 0)
            };
        }
    }
}
