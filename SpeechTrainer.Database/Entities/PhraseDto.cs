using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTrainer.Database.Entities
{
    public class PhraseDto
    {
        public int Id { get; private set; }
        public string Text { get; private set; }

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
