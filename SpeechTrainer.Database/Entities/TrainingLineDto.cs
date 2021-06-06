namespace SpeechTrainer.Database.Entities
{
    public class TrainingLineDto
    {
        public int Id { get; }
        public string StudentAnswer { get; }
        public string CompleteForm { get; }
        public int TrainingId { get; }

        public TrainingLineDto(string studentAnswer, string completeForm, int trainingId)
        {
            StudentAnswer = studentAnswer;
            CompleteForm = completeForm;
            TrainingId = trainingId;
        }

        public TrainingLineDto(int id, string studentAnswer, string completeForm, int trainingId) : this(studentAnswer, completeForm, trainingId)
        {
            Id = id;
        }

        public TrainingLineDto()
        {
        }

        public override string ToString()
        {
            return StudentAnswer + " " + CompleteForm;
        }
    }
}
