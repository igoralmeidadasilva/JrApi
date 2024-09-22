//using System;
//using JrApi.Domain.Core.Interfaces.Repositories.Mongo;
//using JrApi.Domain.Users;
//using MediatR;

//namespace JrApi.Application.Commands.UserMongo.DeleteUserMongo
//{
//    public sealed class DeleteUsersMongoCommandHandler : IRequestHandler<DeleteUserMongoCommand, bool>
//    {
//        private readonly IMongoRepository<User> _mongo;

//        public DeleteUsersMongoCommandHandler(IMongoRepository<User> mongo)
//        {
//            _mongo = mongo;
//        }

//        public async Task<bool> Handle(DeleteUserMongoCommand request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var result = await _mongo.Delete(request.Id);
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
