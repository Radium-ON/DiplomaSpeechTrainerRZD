using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.Core.DtoMappers
{
    public static class TrainingMapper
    {
        public static TrainingDto ConvertToDto(TrainingObservable observable)
        {
            return new TrainingDto(
                observable.Id,
                observable.ScoresNumber,
                observable.TrainingDate,
                observable.StudentId,
                observable.ParticipantId,
                SituationMapper.ConvertToDto(observable.Situation),
                TrainingLineMapper.ConvertToListDto(observable.TrainingLines)?.ToList()
                );
        }

        public static TrainingObservable ConvertFromDto(TrainingDto dto)
        {
            return new TrainingObservable(
            dto.Id,
            dto.StudentId,
            dto.ParticipantId,
            dto.ScoresNumber,
            dto.TrainingDate,
            SituationMapper.ConvertFromDto(dto.Situation),
            TrainingLineMapper.ConvertFromListDto(dto.TrainingLines)?.ToList()
            );
        }

        public static IEnumerable<TrainingDto> ConvertToListDto(List<TrainingObservable> observables)
        {
            return observables?.Select(ConvertToDto);
        }

        public static IEnumerable<TrainingObservable> ConvertFromListDto(List<TrainingDto> dtos)
        {
            return dtos?.Select(ConvertFromDto);
        }
    }
}
