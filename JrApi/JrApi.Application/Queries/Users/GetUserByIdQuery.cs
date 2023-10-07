using System;
using JrApi.Application.Queries.Interfaces;
using JrApi.Domain.Entities;
using MediatR;

namespace JrApi.Application.Queries.Users
{
    public sealed class GetUserByIdQuery : IQuery, IRequest<UserModel>
    {
        public int Id { get; set; }    
    }
}