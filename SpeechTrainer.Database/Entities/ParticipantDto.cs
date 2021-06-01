using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTrainer.Database.Entities
{
    public class ParticipantDto
    {
        public int Id { get; private set; }
        public PositionDto Position { get; private set; }
        public string SituationName { get; private set; }
        public string SituationDescription { get; private set; }

        public ParticipantDto(string situationName, string situationDescription)
        {
            SituationName = situationName;
            SituationDescription = situationDescription;
        }

        public ParticipantDto(int id, string situationName, string situationDescription): this(situationName,situationDescription)
        {
            Id = id;
        }

        public ParticipantDto(int id, string situationName, string situationDescription, PositionDto position): this(id, situationName,situationDescription)
        {
            Position = position;
        }

        public void SetPosition(PositionDto position)
        {
            Position = position;
        }

        public override string ToString()
        {
            return SituationName + " " + SituationDescription + " " + Position.ShortName;
        }
    }
}
