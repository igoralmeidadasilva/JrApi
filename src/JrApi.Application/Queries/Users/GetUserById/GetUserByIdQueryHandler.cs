using AutoMapper;
using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Core.Interfaces.Services;
using JrApi.Domain.Entities.Users;
using Microsoft.Extensions.Logging;

namespace JrApi.Application.Queries.Users.GetUserById;

public sealed class GetUserByIdQueryHandler(
    ILogger<GetUserByIdQueryHandler> logger,
    IMapper mapper,
    IUserReadOnlyRepository userRepository,
    ILinkGeneratorService linkGeneratorService) : IQueryHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
{
    private readonly ILogger<GetUserByIdQueryHandler> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly IUserReadOnlyRepository _userRepository = userRepository;
    private readonly ILinkGeneratorService _linkGeneratorService = linkGeneratorService;

    public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if(user is null)
        {
            _logger.LogInformation("{RequestName} User with Id {UserId} not found.",
                nameof(GetUserByIdQuery),
                request.Id);
            return GetUserByIdQueryResponse.Failure(DomainErrors.User.IdNotFound);
        }

        GetUserByIdQueryResponseItem mapperUser = _mapper.Map<GetUserByIdQueryResponseItem>(user);
        mapperUser.Links = _linkGeneratorService.CreateLinksCollection(nameof(User), request.Id);
        GetUserByIdQueryResponse response = GetUserByIdQueryResponse.Success(mapperUser);

        _logger.LogInformation("{RequestName} Registration recovery for user {UserId} completed successfully.",
            nameof(GetUserByIdQuery),
            request.Id);
        
        return response;
    }
}