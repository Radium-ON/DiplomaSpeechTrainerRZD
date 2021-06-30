using System.Collections.Generic;
using System.Linq;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.Core.DtoMappers
{
    public static class AvailableValueMapper
    {
        public static AvailableValueDto ConvertToDto(AvailableValueObservable observable)
        {
            return new AvailableValueDto(
                observable.Id,
                observable.Value,
                ParameterTypeMapper.ConvertToDto(observable.ParameterType)
                );
        }
        public static AvailableValueObservable ConvertFromDto(AvailableValueDto dto)
        {
            return new AvailableValueObservable(
                dto.Id,
                dto.Value,
                ParameterTypeMapper.ConvertFromDto(dto.ParameterType)
                );
        }
        public static IEnumerable<AvailableValueDto> ConvertToListDto(List<AvailableValueObservable> observables)
        {
            return observables?.Select(ConvertToDto);
        }
        public static IEnumerable<AvailableValueObservable> ConvertFromListDto(List<AvailableValueDto> dtos)
        {
            return dtos?.Select(ConvertFromDto);
        }

    }
}
