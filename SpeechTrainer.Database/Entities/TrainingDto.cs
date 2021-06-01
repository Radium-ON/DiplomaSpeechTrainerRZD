using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTrainer.Database.Entities
{
    public class TrainingDto
    {
        public int Id { get; private set; }
        public int ScoresNumber { get; private set; }
        public DateTime TrainingDate { get; private set; }
        public StudentDto Student { get; private set; }
        public ParticipantDto Participant { get; private set; }
        public List<TrainingLineDto> TrainingLines { get; private set; }

        public TrainingDto(int scores, DateTime date, StudentDto student, ParticipantDto participant)
        {
            ScoresNumber = scores;
            TrainingDate = date;
            Student = student;
            Participant = participant;
        }

        public TrainingDto(int id, int scores, DateTime date, StudentDto student, ParticipantDto participant) : this(scores, date, student, participant)
        {
            Id = id;
        }

        public TrainingDto(int id, int scores, DateTime date, StudentDto student, ParticipantDto participant, List<TrainingLineDto> lines) : this(id, scores, date, student, participant)
        {
            TrainingLines = lines;
        }

        public void SetTrainings(List<TrainingLineDto> trainings)
        {
            TrainingLines = trainings;
        }

        public override string ToString()
        {
            return Student.ToString() + ScoresNumber + " " + TrainingDate;
        }
    }
}
