using System;
using JrApi.Domain.Interfaces.Repositories;
using JrApi.Domain.Models;
using MediatR;

namespace JrApi.Application.Queries.MongoDB.GetAllUsersMongo
{
    public sealed class GetAllUsersMongoQueryHandler : IRequestHandler<GetAllUsersMongoQuery, IEnumerable<UserModel>>
    {
        private readonly IMongoDbRepository<UserModel> _mongo;

        public GetAllUsersMongoQueryHandler(IMongoDbRepository<UserModel> mongo)
        {
            _mongo = mongo;
        }


        public Task<IEnumerable<UserModel>> Handle(GetAllUsersMongoQuery request, CancellationToken cancellationToken)
        {
            var result = _mongo.GetItems();
            if(result is null)
            {
                return default!;
            }
            return result;
        }
    }
}
