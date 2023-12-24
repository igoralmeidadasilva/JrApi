using System;
using JrApi.Domain.Models;
using MediatR;

namespace JrApi.Application.Queries.Users.GetAllUsers
{
    public sealed class GetAllUsersQuery : IRequest<IEnumerable<UserModel>>
    {
        
    }
}
