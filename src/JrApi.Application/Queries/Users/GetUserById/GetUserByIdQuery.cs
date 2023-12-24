using System;
using JrApi.Domain.Models;
using MediatR;

namespace JrApi.Application.Queries.Users.GetUserById
{
    public sealed class GetUserByIdQuery : IRequest<UserModel>
    {
        public int Id { get; set; }    
    }
}
