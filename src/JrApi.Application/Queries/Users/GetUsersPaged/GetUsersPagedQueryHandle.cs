using AutoMapper;
using JrApi.Application.Dtos;
using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Core.Interfaces.Services;
using JrApi.Domain.Entities.Users;
using JrApi.SharedKernel;
using Microsoft.Extensions.Logging;

namespace JrApi.Application.Queries.Users.GetUsersPaged;

public sealed class GetUsersPagedQueryHandle(
    ILogger<GetUsersPagedQueryHandle> logger,
    IMapper mapper,
    IUserReadOnlyRepository userReadOnlyRepository,
    ILinkGeneratorService linkGeneratorService) : IQueryHandler<GetUsersPagedQuery, GetUsersPagedQueryResponse>
{
    private readonly ILogger<GetUsersPagedQueryHandle> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly ILinkGeneratorService _linkGeneratorService = linkGeneratorService;

    public async Task<GetUsersPagedQueryResponse> Handle(GetUsersPagedQuery request, CancellationToken cancellationToken)
    {
        PagedList<User> users = await _userReadOnlyRepository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            x => x.Name!.FirstName,
            cancellationToken);

        if(users is null)
        {
            _logger.LogInformation("{RequestName} user list is null.", 
                nameof(GetUsersPagedQuery));
            return GetUsersPagedQueryResponse.Failure(DomainErrors.User.NoneCanBeFound);
        }
        _logger.LogInformation("{RequestName} Found {UsersCount} User records.", 
            nameof(GetUsersPagedQuery), 
            users.Count);

        IEnumerable<GetUsersPagedQueryResponseItem> mappedUsers = users.Select(_mapper.Map<GetUsersPagedQueryResponseItem>);
        Uri nextPage = _linkGeneratorService.CreatePaginationUri(nameof(User), users.PageNumber + 1, users.PageSize);
        Uri previousPage = _linkGeneratorService.CreatePaginationUri(nameof(User), users.PageNumber - 1, users.PageSize);
        var pagedResponse = new PagedResponseDto<GetUsersPagedQueryResponseItem>
        {
            Result = mappedUsers,
            Count = users.TotalCount,
            Next = users.HasNext ? nextPage.AbsoluteUri : string.Empty,
            Previous = users.HasPrevious ? previousPage.AbsoluteUri : string.Empty,
        };
        return GetUsersPagedQueryResponse.Success(pagedResponse);
    }
}