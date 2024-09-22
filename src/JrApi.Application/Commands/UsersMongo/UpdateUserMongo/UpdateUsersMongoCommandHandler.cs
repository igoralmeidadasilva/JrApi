//using System;
//using JrApi.Domain.Core.Interfaces.Repositories.Mongo;
//using JrApi.Domain.Users;
//using MediatR;

//namespace JrApi.Application.Commands.UserMongo.UpdateUserMongo
//{
//    public sealed class UpdateUsersMongoCommandHandler : IRequestHandler<UpdateUserMongoCommand, User>
//    {
//        private readonly IMongoRepository<User> _mongo;

//        public UpdateUsersMongoCommandHandler(IMongoRepository<User> mongo)
//        {
//            _mongo = mongo;
//        }

//        public async Task<User> Handle(UpdateUserMongoCommand request, CancellationToken cancellationToken)
//        {
//            var user = await _mongo.GetItemById(request.Id);
            
//            if(user is null)
//            {
//                return default!;
//            }

//            user.Name = request.Name;
//            user.LastName = request.LastName;
//            user.BirthDate = request.BirthDate;

//            var result = await _mongo.Update(user);
//            return result;
//        }
//    }
//}
