using System;

namespace SpeechTrainer.Database.Entities
{
    public class IntervalDTO
    {
        public int Id { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime FinishTime { get; private set; }
        public double Rating { get; private set; }

        public IntervalDTO() { }

        public IntervalDTO(int id, DateTime startTime, DateTime finishTime, double rating)
        {
            Id = id;
            StartTime = startTime;
            FinishTime = finishTime;
            Rating = rating;
        }

        public IntervalDTO(DateTime startTime, DateTime finishTime, double rating)
        {
            StartTime = startTime;
            FinishTime = finishTime;
            Rating = rating;
        }

        public override string ToString()
        {
            return StartTime + " " + FinishTime + " " + Rating;
        }
    }
}
