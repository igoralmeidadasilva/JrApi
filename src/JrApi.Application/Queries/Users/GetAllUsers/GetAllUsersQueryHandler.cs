using AutoMapper;
using JrApi.Application.Dtos;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Entities.Users;
using Microsoft.Extensions.Logging;

namespace JrApi.Application.Queries.Users.GetAllUsers;

public sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, Result<GetAllUsersQueryResponse>>
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


    public async Task<Result<GetAllUsersQueryResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<User> users = await _userReadOnlyRepository.GetAllAsync(cancellationToken);

        _logger.LogInformation("{RequestName} Found {UsersCount} User records.", 
            nameof(GetAllUsersQuery), 
            users.Count());
        
        var mappedUsers = users.Select(_mapper.Map<GetAllUsersDto>);
        var response = new GetAllUsersQueryResponse(mappedUsers);

        return Result.Success(response);
    }

}
