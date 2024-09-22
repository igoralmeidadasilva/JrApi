//using System;
//using JrApi.Domain.Core.Interfaces.Repositories;
//using JrApi.Domain.Users;
//using MediatR;

//namespace JrApi.Application.Commands.Users.DeleteUser
//{
//    public sealed class DeleteUsersCommandHandler : IRequestHandler<DeleteUserCommand, bool>
//    {
//        private readonly IDbRepository<User> _user;
//        public DeleteUsersCommandHandler(IDbRepository<User> user)
//        {
//            _user = user;
//        }

//        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var result = await _user.Delete(request.Id);
//                if(!result)
//                {
//                    return false;
//                }
//                return true;
//            } 
//            catch
//            {
//                return false;
//            }
//        }
//    }
//}
