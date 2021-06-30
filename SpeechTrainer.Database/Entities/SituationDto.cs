using System.Collections.Generic;

namespace SpeechTrainer.Database.Entities
{
    public class SituationDto
    {
        public int Id { get; }
        public List<PositionDto> Positions { get; private set; }
        public List<AnswerFormDto> AnswerForms { get; private set; }
        public string Name { get; }
        public string Description { get; }

        public SituationDto(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public SituationDto(int id, string situationName, string situationDescription) : this(situationName, situationDescription)
        {
            Id = id;
        }

        public SituationDto(int id, string situationName, string situationDescription, List<PositionDto> positions) : this(id, situationName, situationDescription)
        {
            Positions = positions;
        }

        public SituationDto(int id, string situationName, string situationDescription, List<PositionDto> positions, List<AnswerFormDto> forms) : this(id, situationName, situationDescription, positions)
        {
            AnswerForms = forms;
        }

        public SituationDto()
        {
        }

        public void SetPositions(List<PositionDto> positions)
        {
            Positions = positions;
        }
        public void SetAnswerForms(List<AnswerFormDto> forms)
        {
            AnswerForms = forms;
        }

        public override string ToString()
        {
            return Name + " " + Description;
        }
    }
}
