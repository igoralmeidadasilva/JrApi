using JrApi.Application.Dtos;

namespace JrApi.Application.Queries.Users.GetUserById;

public sealed record GetUserByIdQueryResponse
{
    public GetUserByIdDto? User { get; init; }

    public GetUserByIdQueryResponse(GetUserByIdDto? user)
    {
        User = user;
    }
}
