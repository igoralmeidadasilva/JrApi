using AutoMapper;
using JrApi.Application.Core.Interfaces;
using JrApi.Domain.Core.Abstractions.Results;
using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces;
using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JrApi.Application.Commands.Users.DeleteUser;

public sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, Result<Unit>>
{
    private readonly ILogger<DeleteUserCommandHandler> _logger;
    private readonly IUserPersistenceRepository _userPersistenceRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteUserCommandHandler(
        ILogger<DeleteUserCommandHandler> logger,
        IUserPersistenceRepository userPersistenceRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _logger = logger;
        _userPersistenceRepository = userPersistenceRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if(!await _userReadOnlyRepository.ExistsAsync(request.Id, cancellationToken))
        {
            _logger.LogInformation("{RequestName} User with Id {UserId} not found.",
                nameof(DeleteUserCommandHandler),
                request.Id);

            return Result.Failure<Unit>(DomainErrors.User.IdNotFound);
        }

        _userPersistenceRepository.Delete(request.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("{RequestName} User with ID {UserId} has been successfully deleted.",
            nameof(DeleteUserCommandHandler),
            request.Id);

        return Result.Success(Unit.Value);
    }
}
