using System;
using JrApi.Application.Queries.Users;
using JrApi.Domain.Interfaces.Repositories;
using JrApi.Domain.Models;
using MediatR;

namespace JrApi.Application.Queries.Users.GetUserById
{
    public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserModel>
    {
        private readonly IDbRepository<UserModel> _repository;

        public GetUserByIdQueryHandler(IDbRepository<UserModel> repository)
        {
            _repository = repository;
        }

        public async Task<UserModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetItemById(request.Id);
            if(result is null)
            {
                return default!;
            }   
            return result;
        }
    }
}
