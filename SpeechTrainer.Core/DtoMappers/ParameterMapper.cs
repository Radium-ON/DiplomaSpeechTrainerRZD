using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.Utills;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.Core.DtoMappers
{
    public static class ParameterMapper
    {
        public static ParameterDto ConvertToDto(ParameterObservable observable)
        {
            return new ParameterDto(
                observable.Id,
                observable.OrderNum,
                AvailableValueMapper.ConvertToDto(observable.Value),
                observable.AnswerFormId
                );
        }
        public static ParameterObservable ConvertFromDto(ParameterDto dto)
        {
            return new ParameterObservable(
                dto.Id,
                dto.OrderNum,
                dto.AnswerFormId,
                AvailableValueMapper.ConvertFromDto(dto.Value)
                );
        }
        public static IEnumerable<ParameterDto> ConvertToListDto(List<ParameterObservable> observables)
        {
            return observables?.Select(ConvertToDto);
        }
        public static IEnumerable<ParameterObservable> ConvertFromListDto(List<ParameterDto> dtos)
        {
            return dtos?.Select(ConvertFromDto);
        }
    }
}
