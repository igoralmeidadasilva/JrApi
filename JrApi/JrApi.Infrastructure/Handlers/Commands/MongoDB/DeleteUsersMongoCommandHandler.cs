using System;
using Amazon.Runtime.Internal;
using JrApi.Application.Commands.MongoDB;
using JrApi.Domain.Entities;
using JrApi.Infrastructure.Services.Interfaces;
using MediatR;

namespace JrApi.Infrastructure.Handlers.Commands.MongoDB
{
    public sealed class DeleteUsersMongoCommandHandler : IRequestHandler<DeleteUserMongoCommand, bool>
    {
        private readonly IMongoDbServices<UserModel> _mongo;

        public DeleteUsersMongoCommandHandler(IMongoDbServices<UserModel> mongo)
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
