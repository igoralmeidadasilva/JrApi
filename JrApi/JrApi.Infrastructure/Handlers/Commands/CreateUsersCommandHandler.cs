using FluentValidation;
using JrApi.Application.Commands.Users;
using JrApi.Domain.Entities;
using JrApi.Infrastructure.Repository.Interfaces;
using MediatR;

namespace JrApi.Infrastructure.Handlers.Commands{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserModel>
    {
        private readonly IDbRepository<UserModel> _user;

        public CreateUserCommandHandler(IDbRepository<UserModel> user)
        {
            _user = user;
        }

        public Task<UserModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {   
            var user = new UserModel (
                request.Name, 
                request.LastName, 
                request.BirthDate);
                
            _user.Insert(user);
            return Task.FromResult(user);
        }
    }
}
