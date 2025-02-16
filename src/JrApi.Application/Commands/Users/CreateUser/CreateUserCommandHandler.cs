using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces;
using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Core.Interfaces.Services;
using JrApi.Domain.Entities.Users;
using Microsoft.Extensions.Logging;

namespace JrApi.Application.Commands.Users.CreateUser;

public sealed class CreateUserCommandHandler(
    ILogger<CreateUserCommandHandler> logger,
    IPasswordHashingService passwordHasher,
    IUserPersistenceRepository userPersistenceRepository,
    IUserReadOnlyRepository userReadOnlyRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateUserCommand, CreateUserCommandResponse>
{
   private readonly ILogger<CreateUserCommandHandler> _logger = logger;
   private readonly IPasswordHashingService _passwordHasher = passwordHasher;
   private readonly IUserPersistenceRepository _userPersistenceRepository = userPersistenceRepository;
   private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
   private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
   {
        if(await _userReadOnlyRepository.EmailExistsAsync(request.Email, cancellationToken))
        {
            _logger.LogInformation("{RequestName} User email already exists.",
                nameof(CreateUserCommand));
            return CreateUserCommandResponse.Failure(DomainErrors.User.EmailAlreadyExists);
        }

        var name = Name.Create(request.FirstName, request.LastName);
        var email = Email.Create(request.Email);
        var password = PasswordHash.Create(request.Password).Hashing(_passwordHasher);
        var address = Address.Create(
            request.Street,
            request.City,
            request.District,
            request.Number,
            request.State,
            request.Country,
            request.ZipCode);

        var user = User.Create(name, email, password, request.BirthDate, address);
        _userPersistenceRepository.Insert(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("{RequestName} User was entered with id {UserId}.", 
            nameof(CreateUserCommand),
            user.Id);
        return CreateUserCommandResponse.Success(user.Id);
   }
}