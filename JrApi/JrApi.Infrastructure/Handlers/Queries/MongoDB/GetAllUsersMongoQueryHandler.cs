using System;
using JrApi.Application.Queries.Interfaces;
using JrApi.Application.Queries.MongoDB;
using JrApi.Domain.Entities;
using JrApi.Infrastructure.Services.Interfaces;
using MediatR;

namespace JrApi.Infrastructure.Handlers.Queries.MongoDB
{
    public sealed class GetAllUsersMongoQueryHandler : IQuery, IRequestHandler<GetAllUsersMongoQuery, IEnumerable<UserModel>>
    {
        private readonly IMongoDbServices<UserModel> _mongo;

        public GetAllUsersMongoQueryHandler(IMongoDbServices<UserModel> mongo)
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
