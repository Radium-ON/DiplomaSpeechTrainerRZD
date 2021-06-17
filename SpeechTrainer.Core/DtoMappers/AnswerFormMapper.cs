using System.Collections.Generic;
using System.Linq;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.Core.DtoMappers
{
    public static class AnswerFormMapper
    {
        public static AnswerFormDto ConvertToDto(AnswerFormObservable observable)
        {
            return new AnswerFormDto(
                observable.Id,
                observable.OrderNum,
                PhraseMapper.ConvertToDto(observable.Phrase),
                SituationMapper.ConvertToDto(observable.Situation),
                ParameterMapper.ConvertToListDto(observable.Parameters)?.ToList(),
                PositionMapper.ConvertToDto(observable.Position)
                );
        }
        public static AnswerFormObservable ConvertFromDto(AnswerFormDto dto)
        {
            return new AnswerFormObservable(
                dto.Id,
                dto.OrderNum,
                PhraseMapper.ConvertFromDto(dto.Phrase),
                SituationMapper.ConvertFromDto(dto.Situation),
                PositionMapper.ConvertFromDto(dto.Position),
                ParameterMapper.ConvertFromListDto(dto.Parameters)?.ToList()
            );
        }
        public static IEnumerable<AnswerFormDto> ConvertToListDto(List<AnswerFormObservable> observables)
        {
            return observables?.Select(ConvertToDto);
        }
        public static IEnumerable<AnswerFormObservable> ConvertFromListDto(List<AnswerFormDto> dtos)
        {
            return dtos?.Select(ConvertFromDto);
        }
    }
}
