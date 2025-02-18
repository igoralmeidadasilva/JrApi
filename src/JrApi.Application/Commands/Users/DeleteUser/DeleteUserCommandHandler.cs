using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces;
using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using Microsoft.Extensions.Logging;

namespace JrApi.Application.Commands.Users.DeleteUser;

public sealed class DeleteUserCommandHandler(
    ILogger<DeleteUserCommandHandler> logger,
    IUserPersistenceRepository userPersistenceRepository,
    IUserReadOnlyRepository userReadOnlyRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteUserCommand, DeleteUserCommandResponse>
{
    private readonly ILogger<DeleteUserCommandHandler> _logger = logger;
    private readonly IUserPersistenceRepository _userPersistenceRepository = userPersistenceRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userReadOnlyRepository.ExistsAsync(request.Id, cancellationToken))
        {
            _logger.LogInformation("{RequestName} User with Id {UserId} not found.",
                nameof(DeleteUserCommandHandler),
                request.Id);
            return DeleteUserCommandResponse.Failure(DomainErrors.User.IdNotFound);
        }

        _userPersistenceRepository.Delete(request.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("{RequestName} User with ID {UserId} has been successfully deleted.",
            nameof(DeleteUserCommandHandler),
            request.Id);
        return DeleteUserCommandResponse.Success(Unit.Value);
    }
}