//using System;
//using JrApi.Application.Queries.Users;
//using JrApi.Domain.Core.Interfaces.Repositories;
//using JrApi.Domain.Users;
//using MediatR;

//namespace JrApi.Application.Queries.Users.GetUserById
//{
//    public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
//    {
//        private readonly IDbRepository<User> _repository;

//        public GetUserByIdQueryHandler(IDbRepository<User> repository)
//        {
//            _repository = repository;
//        }

//        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
//        {
//            var result = await _repository.GetItemById(request.Id);
//            if(result is null)
//            {
//                return default!;
//            }   
//            return result;
//        }
//    }
//}
