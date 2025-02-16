using AutoMapper;
using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Models;
using Microsoft.Extensions.Logging;

namespace JrApi.Application.Queries.Users.GetUserById;

public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
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

    public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if(user is null)
        {
            _logger.LogInformation("{RequestName} User with Id {UserId} not found.",
                nameof(GetUserByIdQuery),
                request.Id);
            return GetUserByIdQueryResponse.Failure(DomainErrors.User.IdNotFound);
        }

        var mapperUser = _mapper.Map<GetUserByIdQueryResponseItem>(user);
        var response = GetUserByIdQueryResponse.Success(mapperUser);

        response.Value!.Links = GenerateUserLinks(request.Id);

        _logger.LogInformation("{RequestName} Registration recovery for user {UserId} completed successfully.",
            nameof(GetUserByIdQuery),
            request.Id);
        
        return response;
    }

    private static IEnumerable<Link> GenerateUserLinks(Guid id)
    {
        IEnumerable<Link> links =
        [
            new($"/api/users/{id}","self", HttpMethod.Get.ToString()),
            new("/api/users", "all-users", HttpMethod.Get.ToString()),
            new("/api/users", "create", HttpMethod.Post.ToString()),
            new($"/api/users/{id}", "update", HttpMethod.Put.ToString()),
            new($"/api/users/{id}", "delete", HttpMethod.Delete.ToString())
        ]; 
        return links;
    }
}