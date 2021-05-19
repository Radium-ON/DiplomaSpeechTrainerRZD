using System;

namespace SpeechTrainer.Core.Model
{
    public class TypeEntity
    {
        public int Id { get; }
        public string Title { get; }

        public TypeEntity(string title)
        {
            if (title.Trim().Length > 0)
            {
                Title = title.Trim();
            }
            else
            {
                throw new Exception("Недопустимый параметр title");
            }

        }
        public TypeEntity(int id, string title) : this(title)
        {
            if (id >= 0)
            {
                Id = id;
            }
            else
            {
                throw new Exception("Недопустимый параметр id");
            }

        }
    }
}
