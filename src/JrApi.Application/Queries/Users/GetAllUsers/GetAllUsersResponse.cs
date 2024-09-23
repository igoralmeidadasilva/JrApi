using JrApi.Application.Dtos;

namespace JrApi.Application.Queries.Users.GetAllUsers;

public sealed record GetAllUsersQueryResponse
{
    public IEnumerable<GetAllUsersDto>? Users { get; init; }

    public GetAllUsersQueryResponse(IEnumerable<GetAllUsersDto>? users)
    {
        Users = users;
    }

}


