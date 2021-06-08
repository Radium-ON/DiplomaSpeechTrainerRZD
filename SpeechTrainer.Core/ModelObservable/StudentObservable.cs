using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SpeechTrainer.Core.ModelObservable
{
    public class StudentObservable : ObservableValidator
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string StudentCode { get; }
        public GroupObservable Group { get; private set; }
        public List<TrainingObservable> Trainings { get; private set; }

        public StudentObservable(int id, string firstName, string lastName, string studentCode, GroupObservable @group, List<TrainingObservable> trainings)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            StudentCode = studentCode;
            Group = @group;
            Trainings = trainings;
        }
    }
}
