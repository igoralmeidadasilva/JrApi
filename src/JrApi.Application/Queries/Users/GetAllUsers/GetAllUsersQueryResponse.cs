using JrApi.SharedKernel.Results;

namespace JrApi.Application.Queries.Users.GetAllUsers;

public sealed class GetAllUsersQueryResponse: Result<IEnumerable<GetAllUsersQueryResponseItem>>, IQueryResponse
{
    public GetAllUsersQueryResponse() { }

    private GetAllUsersQueryResponse(
        IEnumerable<GetAllUsersQueryResponseItem> value, 
        bool isSuccess, 
        IList<Error> errors) 
        : base(value, isSuccess, errors) { }
    
    public static new GetAllUsersQueryResponse Success(IEnumerable<GetAllUsersQueryResponseItem> data) => new(data, true, []);
    public static new GetAllUsersQueryResponse Failure(Error error) => new(default!, true, [error]);
}   