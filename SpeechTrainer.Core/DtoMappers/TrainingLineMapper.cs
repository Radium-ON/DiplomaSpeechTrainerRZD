using System.Collections.Generic;
using System.Linq;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.Core.DtoMappers
{
    public static class TrainingLineMapper
    {
        public static TrainingLineDto ConvertToDto(TrainingLineObservable observable)
        {
            return new TrainingLineDto(
                observable.Id,
                observable.StudentAnswer,
                observable.CompleteForm,
                observable.TrainingId,
                observable.IsCorrect
                );
        }
        public static TrainingLineObservable ConvertFromDto(TrainingLineDto dto)
        {
            return new TrainingLineObservable(
                dto.Id,
                dto.StudentAnswer,
                dto.CompleteForm,
                dto.TrainingId,
                dto.IsCorrect,
                dto.SituationOrderNum
                );
        }
        public static IEnumerable<TrainingLineDto> ConvertToListDto(List<TrainingLineObservable> observables)
        {
            return observables?.Select(ConvertToDto);
        }
        public static IEnumerable<TrainingLineObservable> ConvertFromListDto(List<TrainingLineDto> dtos)
        {
            return dtos?.Select(ConvertFromDto);
        }
    }
}