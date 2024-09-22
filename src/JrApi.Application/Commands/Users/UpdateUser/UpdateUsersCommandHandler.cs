//using System;
//using JrApi.Domain.Core.Interfaces.Repositories;
//using JrApi.Domain.Users;
//using MediatR;

//namespace JrApi.Application.Commands.Users.UpdateUser
//{
//    public sealed class UpdateUsersCommandHandler : IRequestHandler<UpdateUserCommand, User>
//    {
//        private readonly IDbRepository<User> _user;

//        public UpdateUsersCommandHandler(IDbRepository<User> user)
//        {
//            _user = user;
//        }
//        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
//        {            
//            var user = await _user.GetItemById(request.Id);

//            if(user is null)
//            {
//                return default!;
//            }

//            user.Name = request.Name;
//            user.LastName = request.LastName;
//            user.BirthDate = request.BirthDate;
            
//            var result = await _user.Update(user);
//            return result;

//        }
//    }
//}
