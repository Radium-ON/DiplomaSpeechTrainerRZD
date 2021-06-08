using System.Collections.Generic;
using System.Linq;
using SpeechTrainer.Core.ModelObservable;
using SpeechTrainer.Database.Entities;

namespace SpeechTrainer.Core.Utills
{
    public static class ProfileMapper
    {
        public static ProfileDTO ConvertToDto(Profile profile)
        {
            return new ProfileDTO(
                id: profile.Id,
                firstName: profile.FirstName,
                secondName: profile.SecondName,
                tasks: TaskMapper.ConvertToListDto(profile.Tasks)?.ToList());
        }

        public static Profile ConvertFromDto(ProfileDTO profileDto)
        {
            return new Profile(
                id: profileDto.Id,
                firstName: profileDto.FirstName,
                secondName: profileDto.SecondName,
                tasks: TaskMapper.ConvertFromListDto(profileDto.Tasks)?.ToList());
        }

        public static IEnumerable<Profile> ConvertFromProfilesDTO(List<ProfileDTO> profileDtos)
        {
            return profileDtos.Select(ConvertFromDto);
        }
    }
}