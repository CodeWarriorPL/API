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
            CreateMap<API.Models.UserMeasurement, API.Dto.UserMeasurementDto>();
            CreateMap<API.Dto.UserMeasurementDto, API.Models.UserMeasurement>();
            CreateMap<API.Models.TrainingPlan, API.Dto.TrainingPlanDto>();
            CreateMap<API.Dto.TrainingPlanDto, API.Models.TrainingPlan>();
            CreateMap<API.Models.Training, API.Dto.TrainingWithSetsDto>();
            CreateMap<API.Dto.TrainingWithSetsDto, API.Models.Training>();

        }
    }
}
