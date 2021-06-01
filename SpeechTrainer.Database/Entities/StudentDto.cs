using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTrainer.Database.Entities
{
    public class StudentDto
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string StudentCode { get; private set; }
        public string Group { get; private set; }
        public List<TrainingDto> Trainings { get; private set; }

        public StudentDto(string firstName, string lastName, string group, string studentCode)
        {
            FirstName = firstName;
            LastName = lastName;
            StudentCode = studentCode;
            Group = group;
        }

        public StudentDto(int id, string firstName, string lastName, string group, string studentCode) : this(firstName, lastName, group, studentCode)
        {
            Id = id;
        }

        public StudentDto(int id, string firstName, string lastName, string group, string studentCode, List<TrainingDto> trainings) : this(id, firstName, lastName, group, studentCode)
        {
            Trainings = trainings;
        }

        public void SetTrainings(List<TrainingDto> trainings)
        {
            Trainings = trainings;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
