//using System;
//using JrApi.Domain.Core.Interfaces.Repositories.Mongo;
//using JrApi.Domain.Users;
//using MediatR;

//namespace JrApi.Application.Queries.MongoDB.GetAllUsersMongo
//{
//    public sealed class GetAllUsersMongoQueryHandler : IRequestHandler<GetAllUsersMongoQuery, IEnumerable<User>>
//    {
//        private readonly IMongoRepository<User> _mongo;

//        public GetAllUsersMongoQueryHandler(IMongoRepository<User> mongo)
//        {
//            _mongo = mongo;
//        }


//        public Task<IEnumerable<User>> Handle(GetAllUsersMongoQuery request, CancellationToken cancellationToken)
//        {
//            var result = _mongo.GetItems();
//            if(result is null)
//            {
//                return default!;
//            }
//            return result;
//        }
//    }
//}
