using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SpeechTrainer.Core.ModelObservable
{
    public class TrainingObservable : ObservableValidator
    {
        
        public int Id { get; }
        public int StudentId { get; }
        public int ParticipantId { get; }
        public int ScoresNumber { get; }
        public DateTime TrainingDate { get; }
        public StudentObservable Student { get; set; }
        public SituationObservable Situation { get; set; }
        public List<TrainingLineObservable> TrainingLines { get; set; }

        public TrainingObservable(int id, int studentId, int participantId, int scoresNumber, DateTime trainingDate, StudentObservable student, SituationObservable situation, List<TrainingLineObservable> trainingLines)
        {
            Id = id;
            StudentId = studentId;
            ParticipantId = participantId;
            ScoresNumber = scoresNumber;
            TrainingDate = trainingDate;
            Student = student;
            Situation = situation;
            TrainingLines = trainingLines;
        }

        public TrainingObservable()
        {
        }
    }
}
