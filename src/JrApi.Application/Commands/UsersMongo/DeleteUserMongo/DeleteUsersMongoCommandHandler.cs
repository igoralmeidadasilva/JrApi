using System;
using JrApi.Domain.Interfaces.Repositories;
using JrApi.Domain.Models;
using MediatR;

namespace JrApi.Application.Commands.UserMongo.DeleteUserMongo
{
    public sealed class DeleteUsersMongoCommandHandler : IRequestHandler<DeleteUserMongoCommand, bool>
    {
        private readonly IMongoDbRepository<UserModel> _mongo;

        public DeleteUsersMongoCommandHandler(IMongoDbRepository<UserModel> mongo)
        {
            _mongo = mongo;
        }

        public async Task<bool> Handle(DeleteUserMongoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mongo.Delete(request.Id);
                if(!result)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
