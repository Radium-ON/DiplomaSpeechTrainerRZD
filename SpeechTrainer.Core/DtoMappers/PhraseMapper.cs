using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.Utills;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.Core.DtoMappers
{
    public static class PhraseMapper
    {
        public static PhraseDto ConvertToDto(PhraseObservable observable)
        {
            return new PhraseDto(
                observable.Id,
                observable.Text
                );
        }
        public static PhraseObservable ConvertFromDto(PhraseDto dto)
        {
            return new PhraseObservable(
                dto.Id,
                dto.Text
                );
        }
        public static IEnumerable<PhraseDto> ConvertToListDto(List<PhraseObservable> observables)
        {
            return observables?.Select(ConvertToDto);
        }
        public static IEnumerable<PhraseObservable> ConvertFromListDto(List<PhraseDto> dtos)
        {
            return dtos?.Select(ConvertFromDto);
        }
    }
}
