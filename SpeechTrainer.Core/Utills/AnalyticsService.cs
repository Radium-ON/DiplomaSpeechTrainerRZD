using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SpeechTrainer.Core.ModelObservable;

namespace SpeechTrainer.Core.Utills
{
    public class AnalyticsService
    {
        public int AllAttempts { get; private set; }
        public int ExcellentCount { get; private set; }
        public int CouldBeBetterCount { get; private set; }
        public double CorrectAnswersRatio { get; private set; }
        public SituationObservable ProblemSituation { get; private set; }

        private int _correctAnswers;
        private int _inCorrectAnswers;

        private void ResetValues()
        {
            AllAttempts = 0;
            ExcellentCount = 0;
            CouldBeBetterCount = 0;
            _correctAnswers = 0;
            _inCorrectAnswers = 0;
        }

        public Task CollectAnalyticsAsync(List<TrainingObservable> trainings)
        {
            return Task.Run(() =>
            {
                ResetValues();
                foreach (var attempt in trainings)
                {
                    AllAttempts++;
                    if (attempt.ScoresNumber == 100)
                    {
                        ExcellentCount++;
                    }
                    else
                    {
                        CouldBeBetterCount++;
                        if (attempt.ScoresNumber == 0)
                        {
                            ProblemSituation = attempt.Situation;
                        }
                    }

                    foreach (var line in attempt.TrainingLines)
                    {
                        if (line.IsCorrect)
                        {
                            _correctAnswers++;
                        }
                        else
                        {
                            _inCorrectAnswers++;
                        }
                    }
                }

                CorrectAnswersRatio = _correctAnswers / (double)(_correctAnswers + _inCorrectAnswers);
            });
        }
    }
}
