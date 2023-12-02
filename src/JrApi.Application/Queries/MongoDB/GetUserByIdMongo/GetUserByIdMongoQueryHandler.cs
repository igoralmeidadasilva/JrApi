using System;
using JrApi.Domain.Interfaces.Repositories;
using JrApi.Domain.Models;
using MediatR;

namespace JrApi.Application.Queries.MongoDB.GetUserByIdMongo
{
    public sealed class GetUserByIdMongoQueryHandler : IRequestHandler<GetUserByIdMongoQuery, UserModel>
    {
        private readonly IMongoDbRepository<UserModel> _mongo;

        public GetUserByIdMongoQueryHandler(IMongoDbRepository<UserModel> mongo)
        {
            _mongo = mongo;
        }

        public async Task<UserModel> Handle(GetUserByIdMongoQuery request, CancellationToken cancellationToken)
        {
            var result = await _mongo.GetItemById(request.Id);
            if(result is null)
            {
                return default!;
            }
            return result;
        }
    }
}
