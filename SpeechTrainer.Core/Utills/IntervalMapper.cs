using SpeechTrainer.Core.Model;
using System.Collections.Generic;
using System.Linq;
using SpeechTrainer.Database.Entityes;

namespace SpeechTrainer.Core.Utills
{
    public static class IntervalMapper
    {
        public static IntervalDTO ConvertToDto(Interval interval)
        {
            return new IntervalDTO(
                id: interval.Id,
                startTime: interval.StartTime,
                finishTime: interval.FinishTime,
                rating: interval.Rating);
        }
        public static Interval ConvertFromDto(IntervalDTO interval)
        {
            return new Interval(
                id: interval.Id,
                startTime: interval.StartTime,
                finishTime: interval.FinishTime,
                rating: interval.Rating);
        }
        public static IEnumerable<IntervalDTO> ConvertToListDto(List<Interval> intervals)
        {
            return intervals?.Select(ConvertToDto);
        }
        public static IEnumerable<Interval> ConvertFromListDto(List<IntervalDTO> intervals)
        {
            return intervals?.Select(ConvertFromDto);
        }
    }
}
