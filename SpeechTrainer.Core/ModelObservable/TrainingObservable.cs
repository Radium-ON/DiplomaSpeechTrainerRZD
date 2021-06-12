using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SpeechTrainer.Core.ModelObservable
{
    public class TrainingObservable : ObservableValidator
    {
        private int _id;
        private int _studentId;
        private int _participantId;
        private int _scoresNumber;
        private DateTime _trainingDate;
        private StudentObservable _student;
        private SituationObservable _situation;
        private List<TrainingLineObservable> _trainingLines;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public int StudentId
        {
            get => _studentId;
            set => SetProperty(ref _studentId, value);
        }

        public int ParticipantId
        {
            get => _participantId;
            set => SetProperty(ref _participantId, value);
        }

        public int ScoresNumber
        {
            get => _scoresNumber;
            set => SetProperty(ref _scoresNumber, value);
        }

        public DateTime TrainingDate
        {
            get => _trainingDate;
            set => SetProperty(ref _trainingDate, value);
        }

        public StudentObservable Student
        {
            get => _student;
            set => SetProperty(ref _student, value);
        }

        public SituationObservable Situation
        {
            get => _situation;
            set => SetProperty(ref _situation, value);
        }

        public List<TrainingLineObservable> TrainingLines
        {
            get => _trainingLines;
            set => SetProperty(ref _trainingLines, value);
        }

        public TrainingObservable(int id, int studentId, int participantId, int scoresNumber, DateTime trainingDate, StudentObservable student, SituationObservable situation, List<TrainingLineObservable> trainingLines)
        {
            _id = id;
            _studentId = studentId;
            _participantId = participantId;
            _scoresNumber = scoresNumber;
            _trainingDate = trainingDate;
            Student = student;
            Situation = situation;
            TrainingLines = trainingLines;
        }

        public TrainingObservable()
        {
        }
    }
}
