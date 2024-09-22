//using System;
//using JrApi.Domain.Core.Interfaces.Repositories.Mongo;
//using JrApi.Domain.Users;
//using MediatR;

//namespace JrApi.Application.Commands.UserMongo.CreateUserMongo
//{
//    public sealed class CreateUsersMongoCommandHandler : IRequestHandler<CreateUserMongoCommand, User>
//    {
//        private readonly IMongoRepository<User> _mongo;

//        public CreateUsersMongoCommandHandler(IMongoRepository<User> mongo)
//        {
//            _mongo = mongo;
//        }

//        public Task<User> Handle(CreateUserMongoCommand request, CancellationToken cancellationToken)
//        {
//            var user = new UserModel (
//                request.Name, 
//                request.LastName, 
//                request.BirthDate);
                
//            var result = _mongo.Insert(user);
//            return Task.FromResult(result);
//        }
//    }

//    internal interface IMongoDbServices<T>
//    {
//    }

//}
