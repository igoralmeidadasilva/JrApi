using System;
using JrApi.Application.Queries.Interfaces;
using JrApi.Domain.Entities;
using MediatR;

namespace JrApi.Application.Queries.MongoDB
{
    public sealed class GetAllUsersMongoQuery : IQuery, IRequest<IEnumerable<UserModel>>
    {
        
    }
}
