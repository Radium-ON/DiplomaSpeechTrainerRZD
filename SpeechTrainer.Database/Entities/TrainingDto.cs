using System;
using System.Collections.Generic;

namespace SpeechTrainer.Database.Entities
{
    public class TrainingDto
    {
        public int Id { get; }
        public int StudentId { get; }
        public int ParticipantId { get; }
        public int ScoresNumber { get; }
        public DateTime TrainingDate { get; }
        public SituationDto Situation { get; private set; }
        public List<TrainingLineDto> TrainingLines { get; private set; }

        public TrainingDto(int scores, DateTime date, int studentId, int participantId, SituationDto situation)
        {
            StudentId = studentId;
            ParticipantId = participantId;
            ScoresNumber = scores;
            TrainingDate = date;
            Situation = situation;
        }

        public TrainingDto(int id, int scores, DateTime date, int studentId, int participantId, SituationDto situation) : this(scores, date, studentId, participantId, situation)
        {
            Id = id;
        }

        public TrainingDto(int id, int scores, DateTime date, int studentId, int participantId, SituationDto situation, List<TrainingLineDto> lines) : this(id, scores, date, studentId, participantId, situation)
        {
            TrainingLines = lines;
        }

        public TrainingDto()
        {
        }

        public void SetTrainingLines(List<TrainingLineDto> lines)
        {
            TrainingLines = lines;
        }

        public void SetSituation(SituationDto situation)
        {
            Situation = situation;
        }

        public override string ToString()
        {
            return Situation.Name + " " + ScoresNumber + " " + TrainingDate;
        }
    }
}
