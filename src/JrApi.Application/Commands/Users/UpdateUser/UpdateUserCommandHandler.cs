using JrApi.Application.Commands.Users.CreateUser;
using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces;
using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Entities.Users;
using Microsoft.Extensions.Logging;

namespace JrApi.Application.Commands.Users.UpdateUser;

public sealed class UpdateUserCommandHandler(
    ILogger<CreateUserCommandHandler> logger,
    IUserPersistenceRepository userPersistenceRepository,
    IUserReadOnlyRepository userReadOnlyRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateUserCommand, UpdateUserCommandResponse>
{
   private readonly ILogger<CreateUserCommandHandler> _logger = logger;
   private readonly IUserPersistenceRepository _userPersistenceRepository = userPersistenceRepository;
   private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
   private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
   {
       User userToUpdate = await _userReadOnlyRepository.GetByIdAsync(request.Id, cancellationToken);
       if(userToUpdate is null)
       {
           _logger.LogInformation("{RequestName} User with Id {UserId} not found.",
               nameof(CreateUserCommand),
               request.Id);
           return UpdateUserCommandResponse.Failure(DomainErrors.User.IdNotFound);
       }

       var newName = Name.Create(request.FirstName, request.LastName);
       var newAddress = Address.Create(
           request.Street,
           request.City,
           request.District,
           request.Number,
           request.State,
           request.Country,
           request.ZipCode);
    var newBirthdate = request.BirthDate;

       User userWithNewValues = User.Create(newName, userToUpdate.Email!, userToUpdate.Password!, newBirthdate, newAddress);
       userToUpdate = userToUpdate.Update(userWithNewValues);
       _userPersistenceRepository.Update(userToUpdate);
       await _unitOfWork.SaveChangesAsync(cancellationToken);

       _logger.LogInformation("{RequestName} User with Id {UserId} was updated successfully.", 
           nameof(UpdateUserCommand),
           userToUpdate.Id);
       return UpdateUserCommandResponse.Success(Unit.Value);
   }
}