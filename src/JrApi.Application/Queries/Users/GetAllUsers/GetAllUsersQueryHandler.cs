using AutoMapper;
using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Entities.Users;
using Microsoft.Extensions.Logging;

namespace JrApi.Application.Queries.Users.GetAllUsers;

public sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, GetAllUsersQueryResponse>
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly ILogger<GetAllUsersQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IUserReadOnlyRepository userReadOnlyRepository, ILogger<GetAllUsersQueryHandler> logger, IMapper mapper)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<User> users = await _userReadOnlyRepository.GetAllAsync(cancellationToken);

        if(users is null)
        {
            _logger.LogInformation("{RequestName} user list is null.", 
                nameof(GetAllUsersQuery));
            return GetAllUsersQueryResponse.Failure(DomainErrors.User.NoneCanBeFound);
        }

        _logger.LogInformation("{RequestName} Found {UsersCount} User records.", 
            nameof(GetAllUsersQuery), 
            users.Count());
        
        var mappedUsers = users.Select(_mapper.Map<GetAllUsersQueryResponseItem>);
        return GetAllUsersQueryResponse.Success(mappedUsers);
    }
}