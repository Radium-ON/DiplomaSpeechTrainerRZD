using System.Collections.Generic;

namespace SpeechTrainer.Database.Entities
{
    public class StudentDto
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string StudentCode { get; }
        public GroupDto Group { get; private set; }
        public List<TrainingDto> Trainings { get; private set; }

        public StudentDto(string firstName, string lastName, string studentCode, GroupDto group)
        {
            FirstName = firstName;
            LastName = lastName;
            Group = group;
            StudentCode = studentCode;
        }

        public StudentDto(int id, string firstName, string lastName, string studentCode, GroupDto group) : this(firstName, lastName, studentCode, group)
        {
            Id = id;
        }

        public StudentDto(int id, string firstName, string lastName, string studentCode, GroupDto group, List<TrainingDto> trainings) : this(id, firstName, lastName, studentCode, group)
        {
            Trainings = trainings;
        }

        public StudentDto()
        {
        }

        public void SetTrainings(List<TrainingDto> trainings)
        {
            Trainings = trainings;
        }

        public void SetGroup(GroupDto group)
        {
            Group = group;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + Group;
        }
    }
}
