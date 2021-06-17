using System.Collections.Generic;
using System.Linq;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.Core.DtoMappers
{
    public static class PositionMapper
    {
        public static PositionDto ConvertToDto(PositionObservable observable)
        {
            return new PositionDto(
                observable.Id,
                observable.ShortName,
                observable.FullPosition,
                observable.Responsibilities
                );
        }
        public static PositionObservable ConvertFromDto(PositionDto dto)
        {
            return new PositionObservable(
                dto.Id,
                dto.ShortName,
                dto.FullPosition,
                dto.Responsibilities
                );
        }
        public static IEnumerable<PositionDto> ConvertToListDto(List<PositionObservable> observables)
        {
            return observables?.Select(ConvertToDto);
        }
        public static IEnumerable<PositionObservable> ConvertFromListDto(List<PositionDto> dtos)
        {
            return dtos?.Select(ConvertFromDto);
        }
    }
}
