using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTrainer.Database.Entities
{
    public class TrainingLineDto
    {
        public int Id { get; private set; }
        public string StudentAnswer { get; private set; }
        public string CompleteForm { get; private set; }
        public TrainingDto Training { get; private set; }

        public TrainingLineDto(string studentAnswer, string completeForm, TrainingDto training)
        {
            StudentAnswer = studentAnswer;
            CompleteForm = completeForm;
            Training = training;
        }

        public TrainingLineDto(int id, string studentAnswer, string completeForm, TrainingDto training) : this(studentAnswer, completeForm, training)
        {
            Id = id;
        }

        public void SetTraining(TrainingDto training)
        {
            Training = training;
        }

        public override string ToString()
        {
            return Training.ToString() + StudentAnswer + " " + CompleteForm;
        }
    }
}
