using JrApi.Application.Core.Interfaces;
using JrApi.Domain.Core.Abstractions.Results;
using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces;
using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Core.Interfaces.Services;
using JrApi.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JrApi.Application.Commands.Users.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result<Unit>>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IPasswordHashingService _passwordHasher;
    private readonly IUserPersistenceRepository _userPersistenceRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(
        ILogger<CreateUserCommandHandler> logger,
        IPasswordHashingService passwordHasher,
        IUserPersistenceRepository userPersistenceRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _passwordHasher = passwordHasher;
        _userPersistenceRepository = userPersistenceRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if(await _userReadOnlyRepository.EmailExistsAsync(request.Email, cancellationToken))
        {
            _logger.LogInformation("{RequestName} User email already exists.",
                nameof(CreateUserCommand));

            return Result.Failure<Unit>(DomainErrors.User.EmailAlreadyExists);
        }

        var firstName = FirstName.Create(request.FirstName);
        var lastName = LastName.Create(request.LastName);
        var email = Email.Create(request.Email);
        var password = Password.CreateHashingPassword(request.Password, _passwordHasher);
        var address = Address.Create(
            request.Address.Street,
            request.Address.City,
            request.Address.District,
            request.Address.Number,
            request.Address.State,
            request.Address.Country,
            request.Address.ZipCode);

        var user = User.Create(firstName, lastName, email, password, request.BirthDate, address);

        _userPersistenceRepository.Insert(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("{RequestName} User was entered with id {UserId}.", 
            nameof(CreateUserCommand),
            user.Id);

        return Result.Success(Unit.Value);
    }
}
