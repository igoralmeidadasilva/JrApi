using AutoMapper;
using JrApi.Application.Commands.Users.UpdateUser;
using JrApi.Application.Dtos;
using JrApi.Domain.Entities.Users;

namespace JrApi.Application.Profiles;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UpdateUserCommand, User>()
            .ForMember(x => x.FirstName, opt => opt.MapFrom(src => FirstName.Create(src.FirstName)))
            .ForMember(x => x.LastName, opt => opt.MapFrom(src => LastName.Create(src.LastName)))
            .ForMember(x => x.Address, opt => opt.MapFrom(src => Address.Create
            (
                src.Address.Street, 
                src.Address.City, 
                src.Address.District, 
                src.Address.Number, 
                src.Address.State, 
                src.Address.Country,
                src.Address.ZipCode)
            ));

        CreateMap<User, GetAllUsersDto>();

        CreateMap<User, GetUserByIdDto>()
            .ForMember(x => x.Role, opt => opt.MapFrom(src => src.Role.ToString()))
            .ForMember(x => x.Street, opt => opt.MapFrom(src => src.Address!.Street))
            .ForMember(x => x.City, opt => opt.MapFrom(src => src.Address!.City))
            .ForMember(x => x.District, opt => opt.MapFrom(src => src.Address!.District))
            .ForMember(x => x.Number, opt => opt.MapFrom(src => src.Address!.Number))
            .ForMember(x => x.State, opt => opt.MapFrom(src => src.Address!.State))
            .ForMember(x => x.Country, opt => opt.MapFrom(src => src.Address!.Country))
            .ForMember(x => x.ZipCode, opt => opt.MapFrom(src => src.Address!.ZipCode));;
    }

}
