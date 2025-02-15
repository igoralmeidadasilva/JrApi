using AutoMapper;
using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Entities.Users;
using JrApi.SharedKernel;
using JrApi.SharedKernel.PageList;
using Microsoft.Extensions.Logging;

namespace JrApi.Application.Queries.Users.GetUsersPaged;

public sealed class GetUsersPagedQueryHandle : IQueryHandler<GetUsersPagedQuery, GetUsersPagedQueryResponse>
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly ILogger<GetUsersPagedQueryHandle> _logger;
    private readonly IMapper _mapper;

    public GetUsersPagedQueryHandle(IUserReadOnlyRepository userReadOnlyRepository, ILogger<GetUsersPagedQueryHandle> logger, IMapper mapper)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GetUsersPagedQueryResponse> Handle(GetUsersPagedQuery request, CancellationToken cancellationToken)
    {
        PagedList<User> users = await _userReadOnlyRepository.GetPagedAsync(
            orderBy: x => x.Name!.FirstName,
            pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken);

        if(users is null)
        {
            _logger.LogInformation("{RequestName} user list is null.", 
                nameof(GetUsersPagedQuery));
            return GetUsersPagedQueryResponse.Failure(DomainErrors.User.NoneCanBeFound);
        }

        _logger.LogInformation("{RequestName} Found {UsersCount} User records.", 
            nameof(GetUsersPagedQuery), 
            users.Count);
        var mappedUsers = users.Select(_mapper.Map<GetUsersPagedQueryResponseItem>);
        var pagedResponse = new PagedResponse<GetUsersPagedQueryResponseItem>
        {
            Items = mappedUsers,
            Count = users.TotalCount,
            Next =  users.HasNext ? CreatePageLink(users.PageNumber + 1, users.PageSize) : string.Empty,
            Previous = users.HasPrevious ? CreatePageLink(users.PageNumber - 1, users.PageSize) : string.Empty
        };
        return GetUsersPagedQueryResponse.Success(pagedResponse);
    }

    private static string CreatePageLink(int pageNumber, int pageSize)
    {
        // TODO: Não utilizar a URL Hardcoded aqui
        const string URL_TEMPLATE = "http://localhost:5230/api/v1/users?PageNumber={0}&PageSize={1}"; 
        return string.Format(URL_TEMPLATE, pageNumber, pageSize);
    }
}