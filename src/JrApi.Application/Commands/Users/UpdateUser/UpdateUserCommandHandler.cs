using AutoMapper;
using JrApi.Application.Commands.Users.CreateUser;
using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces;
using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Entities.Users;
using Microsoft.Extensions.Logging;

namespace JrApi.Application.Commands.Users.UpdateUser;

public sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Result<Unit>>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IUserPersistenceRepository _userPersistenceRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(
        ILogger<CreateUserCommandHandler> logger,
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

    public async Task<Result<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userReadOnlyRepository.GetByIdAsync(request.Id, cancellationToken);
        if(user is null)
        {
            _logger.LogInformation("{RequestName} User with Id {UserId} not found.",
                nameof(CreateUserCommand),
                request.Id);

            return Result.Failure<Unit>(DomainErrors.User.IdNotFound);
        }

        var userToUpdate = _mapper.Map<User>(request);

        user = user.Update(userToUpdate);

        _userPersistenceRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("{RequestName} User with Id {UserId} was updated successfully.", 
            nameof(UpdateUserCommand),
            user.Id);

        return Result.Success(Unit.Value);
    }
}
