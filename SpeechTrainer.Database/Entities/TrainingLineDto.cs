namespace SpeechTrainer.Database.Entities
{
    public class TrainingLineDto
    {
        public int Id { get; }
        public string StudentAnswer { get; }
        public string CompleteForm { get; }
        public int TrainingId { get; }
        public bool IsCorrect { get; }

        public TrainingLineDto(string studentAnswer, string completeForm, int trainingId, bool isCorrect)
        {
            StudentAnswer = studentAnswer;
            CompleteForm = completeForm;
            TrainingId = trainingId;
            IsCorrect = isCorrect;
        }

        public TrainingLineDto(int id, string studentAnswer, string completeForm, int trainingId, bool isCorrect) : this(studentAnswer, completeForm, trainingId, isCorrect)
        {
            Id = id;
        }

        public TrainingLineDto()
        {
        }

        public override string ToString()
        {
            return StudentAnswer + " " + CompleteForm + " " + IsCorrect;
        }
    }
}
