using JrApi.Application.Queries.Users.GetAllUsers;
using JrApi.Domain.Interfaces.Repositories;
using JrApi.Domain.Models;
using MediatR;

namespace JrApi.Application.Queries.Users.GetAllUsers
{
    public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserModel>>
    {
        private readonly IDbRepository<UserModel> _user;
        public GetAllUsersQueryHandler(IDbRepository<UserModel> user)
        {
            _user = user;
        }
        public async Task<IEnumerable<UserModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _user.GetItems();
            if(result is null)
            {
                return default!;
            }
            return result;
        }
    }
}
