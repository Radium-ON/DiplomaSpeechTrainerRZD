using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SpeechTrainer.Core.ModelObservable
{
    public class TrainingLineObservable : ObservableValidator
    {
        public int Id { get; }
        public string StudentAnswer { get; set; }
        public string CompleteForm { get; set; }
        public int TrainingId { get; }
        public bool IsCorrect { get; set; }
        public int SituationOrderNum { get; set; }

        public TrainingLineObservable(int id, string studentAnswer, string completeForm, int trainingId, bool isCorrect, int situationOrderNum)
        {
            Id = id;
            StudentAnswer = studentAnswer;
            CompleteForm = completeForm;
            TrainingId = trainingId;
            IsCorrect = isCorrect;
            SituationOrderNum = situationOrderNum;
        }

        public TrainingLineObservable()
        {
            
        }
    }
}