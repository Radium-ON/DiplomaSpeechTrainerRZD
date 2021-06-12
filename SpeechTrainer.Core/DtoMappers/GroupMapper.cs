using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Core.Utills;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.Core.DtoMappers
{
    public static class GroupMapper
    {
        public static GroupDto ConvertToDto(GroupObservable observable)
        {
            return new GroupDto(
                observable.Id,
                observable.GroupName
                );
        }
        public static GroupObservable ConvertFromDto(GroupDto dto)
        {
            return new GroupObservable(
                dto.Id,
                dto.GroupName
                );
        }
        public static IEnumerable<GroupDto> ConvertToListDto(List<GroupObservable> observables)
        {
            return observables?.Select(ConvertToDto);
        }
        public static IEnumerable<GroupObservable> ConvertFromListDto(List<GroupDto> dtos)
        {
            return dtos?.Select(ConvertFromDto);
        }
    }
}
