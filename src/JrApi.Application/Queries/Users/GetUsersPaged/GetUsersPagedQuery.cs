namespace JrApi.Application.Queries.Users.GetUsersPaged;

public sealed record GetUsersPagedQuery : IQuery<GetUsersPagedQueryResponse>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }

    public GetUsersPagedQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}