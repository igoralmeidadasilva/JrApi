using System;
using JrApi.Application.Commands.Users;
using JrApi.Domain.Entities;
using JrApi.Infrastructure.Repository.Interfaces;
using MediatR;

namespace JrApi.Infrastructure.Handlers.Commands
{
    public sealed class DeleteUsersCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IDbRepository<UserModel> _user;
        public DeleteUsersCommandHandler(IDbRepository<UserModel> user)
        {
            _user = user;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _user.Delete(request.Id);
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
