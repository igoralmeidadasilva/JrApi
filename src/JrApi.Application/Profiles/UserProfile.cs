using AutoMapper;
using JrApi.Application.Commands.Users.UpdateUser;
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
    }

}
