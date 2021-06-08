using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SpeechTrainer.Core.ModelObservable
{
    public class TrainingLineObservable : ObservableValidator
    {
        public int Id { get; }
        public string StudentAnswer { get; }
        public string CompleteForm { get; }
        public int TrainingId { get; }
        public bool IsCorrect { get; }

        public TrainingLineObservable(int id, string studentAnswer, string completeForm, int trainingId, bool isCorrect)
        {
            Id = id;
            StudentAnswer = studentAnswer;
            CompleteForm = completeForm;
            TrainingId = trainingId;
            IsCorrect = isCorrect;
        }
    }
}