//using JrApi.Application.Queries.Users.GetAllUsers;
//using JrApi.Domain.Core.Interfaces.Repositories;
//using JrApi.Domain.Users;
//using MediatR;

//namespace JrApi.Application.Queries.Users.GetAllUsers
//{
//    public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
//    {
//        private readonly IDbRepository<User> _user;
//        public GetAllUsersQueryHandler(IDbRepository<User> user)
//        {
//            _user = user;
//        }
//        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
//        {
//            var result = await _user.GetItems();
//            if(result is null)
//            {
//                return default!;
//            }
//            return result;
//        }
//    }
//}
