namespace JrApi.Application.Queries.Users.GetAllUsers;

public sealed record GetAllUsersQueryResponseItem
{
    public Guid Id { get; init; }
    public DateTime CreatedOnUtc { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public DateTime BirthDate { get; init; }
}