using System.Collections.Generic;
using System.Linq;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.Core.DtoMappers
{
    public static class StudentMapper
    {
        public static StudentDto ConvertToDto(StudentObservable observable)
        {
            return new StudentDto(
                observable.Id,
                observable.FirstName,
                observable.LastName,
                observable.StudentCode,
                GroupMapper.ConvertToDto(observable.Group),
                TrainingMapper.ConvertToListDto(observable.Trainings)?.ToList());
        }
        public static StudentObservable ConvertFromDto(StudentDto dto)
        {
            return new StudentObservable(
                dto.Id,
                dto.FirstName,
                dto.LastName,
                dto.StudentCode,
                GroupMapper.ConvertFromDto(dto.Group),
                TrainingMapper.ConvertFromListDto(dto.Trainings)?.ToList()
                );
        }
        public static IEnumerable<StudentDto> ConvertToListDto(List<StudentObservable> observables)
        {
            return observables?.Select(ConvertToDto);
        }
        public static IEnumerable<StudentObservable> ConvertFromListDto(List<StudentDto> dtos)
        {
            return dtos?.Select(ConvertFromDto);
        }
    }
}