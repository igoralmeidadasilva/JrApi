using System;
using JrApi.Application.Commands.Users;
using JrApi.Domain.Entities;
using JrApi.Infrastructure.Repository.Interfaces;
using MediatR;

namespace JrApi.Infrastructure.Handlers.Commands
{
    public sealed class UpdateUsersCommandHandler : IRequestHandler<UpdateUserCommand, UserModel>
    {
        private readonly IDbRepository<UserModel> _user;

        public UpdateUsersCommandHandler(IDbRepository<UserModel> user)
        {
            _user = user;
        }
        public async Task<UserModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {            
            var user = await _user.GetItemById(request.Id);

            if(user is null)
            {
                return default!;
            }

            user.Name = request.Name;
            user.LastName = request.LastName;
            user.BirthDate = request.BirthDate;
            
            var result = await _user.Update(user);
            return result;

        }
    }
}
