using JrApi.Domain.Entities.Users;
using MediatR;

namespace JrApi.Application.Queries.Users.GetUserById
{
    public sealed class GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }    
    }
}
