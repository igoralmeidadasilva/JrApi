using System;
using Amazon.Runtime.Internal;
using JrApi.Application.Queries.MongoDB;
using JrApi.Domain.Entities;
using JrApi.Infrastructure.Services.Interfaces;
using MediatR;

namespace JrApi.Infrastructure.Handlers.Queries.MongoDB
{
    public sealed class GetUserByIdMongoQueryHandler : IRequestHandler<GetUserByIdMongoQuery, UserModel>
    {
        private readonly IMongoDbServices<UserModel> _mongo;

        public GetUserByIdMongoQueryHandler(IMongoDbServices<UserModel> mongo)
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
