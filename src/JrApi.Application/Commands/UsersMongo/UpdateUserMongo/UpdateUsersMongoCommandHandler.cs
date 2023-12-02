using System;
using JrApi.Domain.Interfaces.Repositories;
using JrApi.Domain.Models;
using MediatR;

namespace JrApi.Application.Commands.UserMongo.UpdateUserMongo
{
    public sealed class UpdateUsersMongoCommandHandler : IRequestHandler<UpdateUserMongoCommand, UserModel>
    {
        private readonly IMongoDbRepository<UserModel> _mongo;

        public UpdateUsersMongoCommandHandler(IMongoDbRepository<UserModel> mongo)
        {
            _mongo = mongo;
        }

        public async Task<UserModel> Handle(UpdateUserMongoCommand request, CancellationToken cancellationToken)
        {
            var user = await _mongo.GetItemById(request.Id);
            
            if(user is null)
            {
                return default!;
            }

            user.Name = request.Name;
            user.LastName = request.LastName;
            user.BirthDate = request.BirthDate;

            var result = await _mongo.Update(user);
            return result;
        }
    }
}
