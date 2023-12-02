using System;
using JrApi.Domain.Interfaces.Repositories;
using JrApi.Domain.Models;
using MediatR;

namespace JrApi.Application.Commands.Users.UpdateUser
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
