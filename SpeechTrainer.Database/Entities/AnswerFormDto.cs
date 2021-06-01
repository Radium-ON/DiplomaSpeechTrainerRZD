using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTrainer.Database.Entities
{
    public class AnswerFormDto
    {
        public int Id { get; private set; }
        public int OrderNum { get; private set; }
        public string Text { get; private set; }
        public ParticipantDto Participant { get; private set; }
        public List<ParameterDto> Parameters { get; private set; }

        public AnswerFormDto(int orderNum, string text, ParticipantDto participant)
        {
            Participant = participant;
            OrderNum = orderNum;
            Text = text;
        }

        public AnswerFormDto(int id, int orderNum, string text, ParticipantDto participant) : this(orderNum,text, participant)
        {
            Id = id;
        }

        public AnswerFormDto(int id, int orderNum, string text, ParticipantDto participant, List<ParameterDto> parms): this(id,orderNum,text,participant)
        {
            Parameters = parms;
        }

        public void SetParameters(List<ParameterDto> parms)
        {
            Parameters = parms;
        }

        public void SetParticipant(ParticipantDto participant)
        {
            Participant = participant;
        }

        public override string ToString()
        {
            return OrderNum + " " + Text + " " + Participant.Position.ShortName;
        }
    }
}
