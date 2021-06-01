using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTrainer.Database.Entities
{
    public class PositionDto
    {
        public int Id { get; private set; }
        public string ShortName { get; private set; }
        public string FullPosition { get; private set; }
        public string Responsibilities { get; private set; }

        public PositionDto() { }

        public PositionDto(string shortName, string fullPosition, string responsibilities)
        {
            ShortName = shortName;
            FullPosition = fullPosition;
            Responsibilities = responsibilities;
        }
        
        public PositionDto(int id, string startTime, string finishTime, string responsibilities): this(startTime, finishTime, responsibilities)
        {
            Id = id;
        }


        public override string ToString()
        {
            return ShortName + " " + FullPosition + " " + Responsibilities;
        }
    }
}
