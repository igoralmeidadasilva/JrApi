using JrApi.Application.Dtos;
using JrApi.SharedKernel.Results;

namespace JrApi.Application.Queries.Users.GetUsersPaged;

public sealed class GetUsersPagedQueryResponse: Result<PagedResponseDto<GetUsersPagedQueryResponseItem>>, IQueryResponse
{
    public GetUsersPagedQueryResponse() { }

    private GetUsersPagedQueryResponse(
        PagedResponseDto<GetUsersPagedQueryResponseItem> value, 
        bool isSuccess, 
        IList<Error> errors) 
        : base(value, isSuccess, errors) { }
    
    public static new GetUsersPagedQueryResponse Success(PagedResponseDto<GetUsersPagedQueryResponseItem> data) => new(data, true, []);
    public static new GetUsersPagedQueryResponse Failure(Error error) => new(default!, true, [error]);
}   