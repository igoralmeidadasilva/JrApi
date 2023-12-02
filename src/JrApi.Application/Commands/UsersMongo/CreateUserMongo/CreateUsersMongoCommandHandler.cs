using System;
using JrApi.Domain.Interfaces.Repositories;
using JrApi.Domain.Models;
using MediatR;

namespace JrApi.Application.Commands.UserMongo.CreateUserMongo
{
    public sealed class CreateUsersMongoCommandHandler : IRequestHandler<CreateUserMongoCommand, UserModel>
    {
        private readonly IMongoDbRepository<UserModel> _mongo;

        public CreateUsersMongoCommandHandler(IMongoDbRepository<UserModel> mongo)
        {
            _mongo = mongo;
        }

        public Task<UserModel> Handle(CreateUserMongoCommand request, CancellationToken cancellationToken)
        {
            var user = new UserModel (
                request.Name, 
                request.LastName, 
                request.BirthDate);
                
            var result = _mongo.Insert(user);
            return Task.FromResult(result);
        }
    }

    internal interface IMongoDbServices<T>
    {
    }

}
