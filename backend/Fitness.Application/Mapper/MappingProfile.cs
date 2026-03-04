using AutoMapper;
using Fitness.Application.DTOs.CalendarsDTO;
using Fitness.Application.DTOs.ExercisesDTO;
using Fitness.Application.DTOs.UsersDTO;
using Fitness.Domain.Models;

namespace Fitness.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<CreateUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();

            CreateMap<Calendar, CalendarDTO>().ReverseMap();
            CreateMap<CreateCalendarDTO, Calendar>();
            CreateMap<UpdateCalendarDTO, Calendar>();

            CreateMap<Exercise, ExerciseDTO>().ReverseMap();
            CreateMap<CreateExerciseDTO, Exercise>();
            CreateMap<UpdateExerciseDTO, Exercise>();
        }
    }
}
