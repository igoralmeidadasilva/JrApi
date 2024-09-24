namespace JrApi.Application.Queries.Users.GetUserById;

public record GetUserByIdQuery : IQuery<Result<GetUserByIdQueryResponse>>
{
    public Guid Id { get; init; }

    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }
}
