using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.Utills;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.Core.DtoMappers
{
    public static class ParameterTypeMapper
    {
        public static ParameterTypeDto ConvertToDto(ParameterTypeObservable observable)
        {
            return new ParameterTypeDto(
                observable.Id,
                observable.TypeName
                );
        }
        public static ParameterTypeObservable ConvertFromDto(ParameterTypeDto dto)
        {
            return new ParameterTypeObservable(
                dto.Id,
                dto.TypeName
                );
        }
        public static IEnumerable<ParameterTypeDto> ConvertToListDto(List<ParameterTypeObservable> observables)
        {
            return observables?.Select(ConvertToDto);
        }
        public static IEnumerable<ParameterTypeObservable> ConvertFromListDto(List<ParameterTypeDto> dtos)
        {
            return dtos?.Select(ConvertFromDto);
        }
    }
}
