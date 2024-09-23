using JrApi.Application.Core.Interfaces;
using JrApi.Domain.Core.Abstractions.Results;

namespace JrApi.Application.Queries.Users.GetAllUsers;

public sealed record GetAllUsersQuery : IQuery<Result<GetAllUsersQueryResponse>>
{
    
}

