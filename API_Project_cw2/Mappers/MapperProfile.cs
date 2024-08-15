using API_Project_cw2.Models;
using AutoMapper;
using API_Project_cw2.Models;
using API_Project_cw2.Requests;
using MessengerBackend.DTOs;


namespace API_Project_cw2.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDTO>()
            // .ForMember(dest => dest.Nickname,
            //     opt => opt.MapFrom(
            //         src => src.UserName))  -  якщо назви полів моделі і ДТО не співпадають
            .ReverseMap();

        CreateMap<User, CreateUserRequest>().ReverseMap();
    }
}