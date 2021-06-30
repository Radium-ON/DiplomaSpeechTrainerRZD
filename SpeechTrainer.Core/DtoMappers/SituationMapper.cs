using System.Collections.Generic;
using System.Linq;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.Core.DtoMappers
{
    public static class SituationMapper
    {
        public static SituationDto ConvertToDto(SituationObservable observable)
        {
            return new SituationDto(
                observable.Id,
                observable.Name,
                observable.Description,
                PositionMapper.ConvertToListDto(observable.Positions)?.ToList(),
                AnswerFormMapper.ConvertToListDto(observable.AnswerForms)?.ToList()
                );
        }

        public static SituationObservable ConvertFromDto(SituationDto dto)
        {
            return new SituationObservable(
            dto.Id,
            PositionMapper.ConvertFromListDto(dto.Positions)?.ToList(),
            AnswerFormMapper.ConvertFromListDto(dto.AnswerForms)?.ToList(),
            dto.Name,
            dto.Description
                );
        }

        public static IEnumerable<SituationDto> ConvertToListDto(List<SituationObservable> observables)
        {
            return observables?.Select(ConvertToDto);
        }

        public static IEnumerable<SituationObservable> ConvertFromListDto(List<SituationDto> dtos)
        {
            return dtos?.Select(ConvertFromDto);
        }
    }
}