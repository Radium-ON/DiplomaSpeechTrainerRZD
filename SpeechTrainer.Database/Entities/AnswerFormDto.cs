using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTrainer.Database.Entities
{
    public class AnswerFormDto
    {
        public int Id { get; private set; }
        public int OrderNum { get; private set; }
        public PhraseDto Phrase { get; private set; }
        public SituationDto Situation { get; private set; }
        public PositionDto Position { get; private set; }
        public List<ParameterDto> Parameters { get; private set; }

        public AnswerFormDto(int orderNum, PhraseDto text, SituationDto situation)
        {
            Situation = situation;
            OrderNum = orderNum;
            Phrase = text;
        }

        public AnswerFormDto(int id, int orderNum, PhraseDto text, SituationDto situation) : this(orderNum, text, situation)
        {
            Id = id;
        }

        public AnswerFormDto(int id, int orderNum, PhraseDto text, SituationDto situation, List<ParameterDto> parms) : this(id, orderNum, text, situation)
        {
            Parameters = parms;
        }

        public AnswerFormDto(int id, int orderNum, PhraseDto text, SituationDto situation, List<ParameterDto> parms, PositionDto position) : this(id, orderNum, text, situation, parms)
        {
            Position = position;
        }

        public void SetParameters(List<ParameterDto> parms)
        {
            Parameters = parms;
        }

        public void SetSituation(SituationDto situation)
        {
            Situation = situation;
        }

        public void SetPosition(PositionDto position)
        {
            Position = position;
        }

        public override string ToString()
        {
            return OrderNum + " " + Phrase;
        }
    }
}
