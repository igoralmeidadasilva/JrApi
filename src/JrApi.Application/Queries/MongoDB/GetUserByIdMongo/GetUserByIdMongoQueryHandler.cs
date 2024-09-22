//using System;
//using JrApi.Domain.Core.Interfaces.Repositories.Mongo;
//using JrApi.Domain.Users;
//using MediatR;

//namespace JrApi.Application.Queries.MongoDB.GetUserByIdMongo
//{
//    public sealed class GetUserByIdMongoQueryHandler : IRequestHandler<GetUserByIdMongoQuery, User>
//    {
//        private readonly IMongoRepository<User> _mongo;

//        public GetUserByIdMongoQueryHandler(IMongoRepository<User> mongo)
//        {
//            _mongo = mongo;
//        }

//        public async Task<User> Handle(GetUserByIdMongoQuery request, CancellationToken cancellationToken)
//        {
//            var result = await _mongo.GetItemById(request.Id);
//            if(result is null)
//            {
//                return default!;
//            }
//            return result;
//        }
//    }
//}
