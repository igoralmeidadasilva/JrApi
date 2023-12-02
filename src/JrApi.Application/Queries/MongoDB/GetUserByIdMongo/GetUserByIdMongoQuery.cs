using System;
using JrApi.Domain.Models;
using MediatR;

namespace JrApi.Application.Queries.MongoDB.GetUserByIdMongo
{
    public sealed class GetUserByIdMongoQuery : IRequest<UserModel>
    {
        public int Id { get; set; }
    }
}
