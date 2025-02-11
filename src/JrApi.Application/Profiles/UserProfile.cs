using AutoMapper;
using JrApi.Application.Commands.Users.UpdateUser;
using JrApi.Application.Queries.Users.GetAllUsers;
using JrApi.Application.Queries.Users.GetUserById;
using JrApi.Domain.Entities.Users;

namespace JrApi.Application.Profiles;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, GetAllUsersQueryResponseItem>();

        //CreateMap<User, GetUserByIdQueryResponseItem>()
        //    .ForMember(x => x.Role, opt => opt.MapFrom(src => src.Role.ToString()))
        //    .ForMember(x => x.Street, opt => opt.MapFrom(src => src.Address!.Street))
        //    .ForMember(x => x.City, opt => opt.MapFrom(src => src.Address!.City))
        //    .ForMember(x => x.District, opt => opt.MapFrom(src => src.Address!.District))
        //    .ForMember(x => x.Number, opt => opt.MapFrom(src => src.Address!.Number))
        //    .ForMember(x => x.State, opt => opt.MapFrom(src => src.Address!.State))
        //    .ForMember(x => x.Country, opt => opt.MapFrom(src => src.Address!.Country))
        //    .ForMember(x => x.ZipCode, opt => opt.MapFrom(src => src.Address!.ZipCode));;
    }
}