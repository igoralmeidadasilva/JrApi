using System;
using JrApi.Application.Commands.MongoDB;
using JrApi.Domain.Entities;
using JrApi.Infrastructure.Services.Interfaces;
using MediatR;

namespace JrApi.Infrastructure.Handlers.Commands.MongoDB
{
    public sealed class UpdateUsersMongoCommandHandler : IRequestHandler<UpdateUserMongoCommand, UserModel>
    {
        private readonly IMongoDbServices<UserModel> _mongo;

        public UpdateUsersMongoCommandHandler(IMongoDbServices<UserModel> mongo)
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
