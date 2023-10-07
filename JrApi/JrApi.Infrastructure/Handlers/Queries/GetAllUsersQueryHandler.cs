using System;
using JrApi.Application.Queries.Interfaces;
using JrApi.Application.Queries.Users;
using JrApi.Domain.Entities;
using JrApi.Infrastructure.Repository.Interfaces;
using MediatR;

namespace JrApi.Infrastructure.Handlers.Queries
{
    public sealed class GetAllUsersQueryHandler : IQuery, IRequestHandler<GetAllUsersQuery, IEnumerable<UserModel>>
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
