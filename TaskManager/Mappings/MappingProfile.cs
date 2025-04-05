using AutoMapper;
using TaskManager.DTO;
using TaskManager.DTOs;
using TaskManager.Entities;

namespace TaskManager.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskDto, TaskItem>().ReverseMap();
        CreateMap<TaskItem, TaskResponse>().ReverseMap();
        CreateMap<UserRegisterDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
    }
}
