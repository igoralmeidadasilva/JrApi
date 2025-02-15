using JrApi.SharedKernel;
using JrApi.SharedKernel.PageList;
using JrApi.SharedKernel.Results;

namespace JrApi.Application.Queries.Users.GetUsersPaged;

public sealed class GetUsersPagedQueryResponse: Result<PagedResponse<GetUsersPagedQueryResponseItem>>, IQueryResponse
{
    public GetUsersPagedQueryResponse() { }

    private GetUsersPagedQueryResponse(
        PagedResponse<GetUsersPagedQueryResponseItem> value, 
        bool isSuccess, 
        IList<Error> errors) 
        : base(value, isSuccess, errors) { }
    
    public static new GetUsersPagedQueryResponse Success(PagedResponse<GetUsersPagedQueryResponseItem> data) => new(data, true, []);
    public static new GetUsersPagedQueryResponse Failure(Error error) => new(default!, true, [error]);
}   