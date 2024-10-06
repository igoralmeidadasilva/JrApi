using AutoMapper;
using JrApi.Application.Dtos;
using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Models;
using Microsoft.Extensions.Logging;

namespace JrApi.Application.Queries.Users.GetUserById;

public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, Result<GetUserByIdQueryResponse>>
{
    private readonly IUserReadOnlyRepository _userRepository;
    private readonly ILogger<GetUserByIdQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserReadOnlyRepository userRepository, ILogger<GetUserByIdQueryHandler> logger, IMapper mapper)
    {
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
    }


    public async Task<Result<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if(user is null)
        {
            _logger.LogInformation("{RequestName} User with Id {UserId} not found.",
                nameof(GetUserByIdQuery),
                request.Id);

            return Result.Failure<GetUserByIdQueryResponse>(DomainErrors.User.IdNotFound);
        }

        var mapperUser = _mapper.Map<GetUserByIdDto>(user);
        var response = new GetUserByIdQueryResponse(mapperUser);

        response.User!.Links = GenerateUserLinks(request.Id);

        _logger.LogInformation("{RequestName} Registration recovery for user {UserId} completed successfully.",
            nameof(GetUserByIdQuery),
            request.Id);
        
        return Result.Success(response);
    }

    private static IEnumerable<Link> GenerateUserLinks(Guid id)
    {
        IEnumerable<Link> links =
        [
            new() { Rel = "self", Href = $"/api/users/{id}", Method = HttpMethod.Get.ToString() },
            new() { Rel = "all-users", Href = "/api/users", Method = HttpMethod.Get.ToString() },
            new() { Rel = "create", Href = "/api/users", Method = HttpMethod.Post.ToString() },
            new() { Rel = "update", Href = $"/api/users/{id}", Method = HttpMethod.Put.ToString() },
            new() { Rel = "delete", Href = $"/api/users/{id}", Method = HttpMethod.Delete.ToString() }
        ];
        
        return links;
    }
}
