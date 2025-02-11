//using JrApi.Application.Commands.Users.CreateUser;
//using JrApi.Domain.Core.Errors;
//using JrApi.Domain.Core.Interfaces;
//using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
//using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
//using JrApi.Domain.Entities.Users;
//using Microsoft.Extensions.Logging;

//namespace JrApi.Application.Commands.Users.UpdateUser;

//public sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, UpdateUserCommandResponse>
//{
//    private readonly ILogger<CreateUserCommandHandler> _logger;
//    private readonly IUserPersistenceRepository _userPersistenceRepository;
//    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
//    private readonly IUnitOfWork _unitOfWork;

//    public UpdateUserCommandHandler(
//        ILogger<CreateUserCommandHandler> logger,
//        IUserPersistenceRepository userPersistenceRepository,
//        IUserReadOnlyRepository userReadOnlyRepository,
//        IUnitOfWork unitOfWork)
//    {
//        _logger = logger;
//        _userPersistenceRepository = userPersistenceRepository;
//        _userReadOnlyRepository = userReadOnlyRepository;
//        _unitOfWork = unitOfWork;
//    }

//    public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
//    {
//        User user = await _userReadOnlyRepository.GetByIdAsync(request.Id, cancellationToken);
//        if(user is null)
//        {
//            _logger.LogInformation("{RequestName} User with Id {UserId} not found.",
//                nameof(CreateUserCommand),
//                request.Id);
//            return UpdateUserCommandResponse.Failure(DomainErrors.User.IdNotFound);
//        }

//        var name = Name.Create(request.FirstName, request.LastName);
//        var email = Email.Create("");
//        var password = PasswordHash.Create("");
//        var address = Address.Create(
//            request.Address.Street,
//            request.Address.City,
//            request.Address.District,
//            request.Address.Number,
//            request.Address.State,
//            request.Address.Country,
//            request.Address.ZipCode);

//        User userToUpdate = User.Create(name, email, password, request.BirthDate, address);
//        user = user.Update(userToUpdate);
//        _userPersistenceRepository.Update(user);
//        await _unitOfWork.SaveChangesAsync(cancellationToken);

//        _logger.LogInformation("{RequestName} User with Id {UserId} was updated successfully.", 
//            nameof(UpdateUserCommand),
//            user.Id);
//        return UpdateUserCommandResponse.Success(Unit.Value);
//    }
//}