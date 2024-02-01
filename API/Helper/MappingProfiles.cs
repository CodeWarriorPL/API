using API.Dto;
using API.Models;
using AutoMapper;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<API.Models.User, API.Dto.UserDto>();
            CreateMap<API.Models.Training, API.Dto.TrainingDto>();
            CreateMap<API.Dto.TrainingDto, API.Models.Training>();
            CreateMap<API.Models.Exercise, API.Dto.ExerciseDto>();
            CreateMap<API.Dto.ExerciseDto, API.Models.Exercise>();
            CreateMap<API.Models.Set, API.Dto.SetDto>();
            CreateMap<API.Dto.SetDto, API.Models.Set>();

        }
    }
}
