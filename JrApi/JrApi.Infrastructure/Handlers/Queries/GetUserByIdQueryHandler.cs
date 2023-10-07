using System;
using JrApi.Application.Queries.Interfaces;
using JrApi.Application.Queries.Users;
using JrApi.Domain.Entities;
using JrApi.Infrastructure.Repository.Interfaces;
using MediatR;

namespace JrApi.Infrastructure.Handlers.Queries
{
    public sealed class GetUserByIdQueryHandler : IQuery, IRequestHandler<GetUserByIdQuery, UserModel>
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
