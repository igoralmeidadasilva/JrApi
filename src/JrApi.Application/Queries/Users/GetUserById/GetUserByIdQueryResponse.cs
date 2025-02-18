using JrApi.Application.Dtos;
using JrApi.SharedKernel.Results;

namespace JrApi.Application.Queries.Users.GetUserById;

public sealed class GetUserByIdQueryResponse : Result<LinkCollectionResponseDto<GetUserByIdQueryResponseItem>>, IQueryResponse
{
    public GetUserByIdQueryResponse() { }

    private GetUserByIdQueryResponse(
        LinkCollectionResponseDto<GetUserByIdQueryResponseItem> value, 
        bool isSuccess, 
        IList<Error> errors) 
        : base(value, isSuccess, errors) { }

    public static new GetUserByIdQueryResponse Success(LinkCollectionResponseDto<GetUserByIdQueryResponseItem> value) => new(value, true, []);
    public static new GetUserByIdQueryResponse Failure(Error error) => new(default!, false, [error]);
}