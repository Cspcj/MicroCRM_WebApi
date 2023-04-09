using AutoMapper;
using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;

namespace MCRMWebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, UserPartialCreateDTO>().ReverseMap();
            CreateMap<UserDTO, UserDTOUpdate>().ReverseMap();

            CreateMap<ClientDTO, ClientPartialCreateDTO>().ReverseMap();
            CreateMap<ClientDTO, ClientPartialUpdateDTO>().ReverseMap();

            CreateMap<ProjectDTO, ProjectPartialCreateDTO>().ReverseMap();
            CreateMap<ProjectDTO, ProjectPartialUpdateDTO>().ReverseMap();

            CreateMap<EmployeeDTO, EmployeePartialCreateDTO>().ReverseMap();
            CreateMap<EmployeeDTO, EmployeePartialUpdateDTO>().ReverseMap();

            CreateMap<NoteDTO, NotePartialCreateDTO>().ReverseMap();
            CreateMap<NoteDTO, NotePartialUpdateDTO>().ReverseMap();

            CreateMap<TaskDTO, TaskPartialCreateDTO>().ReverseMap();
            CreateMap<TaskDTO, TaskPartialUpdateDTO>().ReverseMap();
        }
    }
}