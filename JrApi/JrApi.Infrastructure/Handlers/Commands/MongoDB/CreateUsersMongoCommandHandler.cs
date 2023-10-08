using System;
using JrApi.Application.Commands.MongoDB;
using JrApi.Domain.Entities;
using JrApi.Infrastructure.Services.Interfaces;
using MediatR;

namespace JrApi.Infrastructure.Handlers.Commands.MongoDB
{
    public sealed class CreateUsersMongoCommandHandler : IRequestHandler<CreateUserMongoCommand, UserModel>
    {
        private readonly IMongoDbServices<UserModel> _mongo;

        public CreateUsersMongoCommandHandler(IMongoDbServices<UserModel> mongo)
        {
            _mongo = mongo;
        }

        public Task<UserModel> Handle(CreateUserMongoCommand request, CancellationToken cancellationToken)
        {
            var user = new UserModel (
                request.Name, 
                request.LastName, 
                request.BirthDate);
                
            var result =_mongo.Insert(user);
            return Task.FromResult(result);
        }
    }
}
