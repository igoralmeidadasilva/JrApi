using System;
using JrApi.Domain.Models;
using MediatR;

namespace JrApi.Application.Queries.MongoDB.GetAllUsersMongo
{
    public sealed class GetAllUsersMongoQuery : IRequest<IEnumerable<UserModel>>
    {
        
    }
}
