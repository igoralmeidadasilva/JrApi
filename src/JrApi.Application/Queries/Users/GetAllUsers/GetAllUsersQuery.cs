using JrApi.Domain.Entities.Users;
using MediatR;

namespace JrApi.Application.Queries.Users.GetAllUsers
{
    public sealed class GetAllUsersQuery : IRequest<IEnumerable<User>>
    {
        
    }
}
