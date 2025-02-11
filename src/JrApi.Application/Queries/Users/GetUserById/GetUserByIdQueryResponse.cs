using JrApi.SharedKernel.Results;

namespace JrApi.Application.Queries.Users.GetUserById;

public sealed class GetUserByIdQueryResponse : Result<GetUserByIdQueryResponseItem>, IQueryResponse
{
    public GetUserByIdQueryResponse() { }

    private GetUserByIdQueryResponse(
        GetUserByIdQueryResponseItem value, 
        bool isSuccess, 
        IList<Error> errors) 
        : base(value, isSuccess, errors) { }

    public static new GetUserByIdQueryResponse Success(GetUserByIdQueryResponseItem value) => new(value, true, []);
    public static new GetUserByIdQueryResponse Failure(Error error) => new(default!, false, [error]);
}