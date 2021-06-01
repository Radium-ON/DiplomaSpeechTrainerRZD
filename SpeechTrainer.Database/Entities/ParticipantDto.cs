using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTrainer.Database.Entities
{
    public class ParticipantDto
    {
        public int Id { get; private set; }
        public string SituationName { get; private set; }
        public string SituationDescription { get; private set; }
        public List<AnswerFormDto> AnswerForms { get; private set; }

        public ParticipantDto(List<AnswerFormDto> answerForms)
        {
            AnswerForms = answerForms;
        }

        public ParticipantDto(string situationName, string situationDescription, List<AnswerFormDto> forms) : this(forms)
        {
            
        }

        public ParticipantDto(int id, string situationName, string situationDescription, List<AnswerFormDto> forms) : this(situationName, situationDescription, forms)
        {
            Id = id;
        }

        public ParticipantDto(string situationName, string situationDescription)
        {
            SituationName = situationName;
            SituationDescription = situationDescription;
        }

        public ParticipantDto(int id, string situationName, string situationDescription): this(situationName,situationDescription)
        {
            Id = id;
        }

        public void SetAnswerForms(List<AnswerFormDto> forms)
        {
            AnswerForms = forms;
        }

        public override string ToString()
        {
            return SituationName + " " + SituationDescription;
        }
    }
}
