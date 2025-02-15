namespace JrApi.Application.Queries.Users.GetUsersPaged;

public sealed record GetUsersPagedQueryResponseItem
{
    public Guid Id { get; init; }
    public string? FullName { get; init; }
}