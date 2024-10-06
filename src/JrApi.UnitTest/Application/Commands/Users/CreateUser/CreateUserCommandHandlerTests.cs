using JrApi.Application.Commands.Users.CreateUser;
using JrApi.Application.Models;
using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces;
using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Core.Interfaces.Services;
using JrApi.Domain.Entities.Users;

namespace JrApi.UnitTest.Application.Commands.Users.CreateUser;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<ILogger<CreateUserCommandHandler>> _loggerMock;
    private readonly Mock<IPasswordHashingService> _passwordHasherMock;
    private readonly Mock<IUserPersistenceRepository> _userPersistenceRepositoryMock;
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreateUserCommandHandler>>();
        _passwordHasherMock = new Mock<IPasswordHashingService>();
        _userPersistenceRepositoryMock = new Mock<IUserPersistenceRepository>();
        _userReadOnlyRepositoryMock = new Mock<IUserReadOnlyRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new CreateUserCommandHandler(
            _loggerMock.Object,
            _passwordHasherMock.Object,
            _userPersistenceRepositoryMock.Object,
            _userReadOnlyRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenEmailAlreadyExists()
    {
        // Arrange
        var address = new AddressCommandModel
        {
            Street = "123 Main St",
            City = "Anytown",
            District = "Downtown",
            Number = 1,
            State = "CA",
            Country = "USA",
            ZipCode = "99888-777"
        };
        var command = new CreateUserCommand("John", "Doe", "john.doe@example.com", "@Password123", DateTime.Now, address);


        _userReadOnlyRepositoryMock
            .Setup(repo => repo.EmailExistsAsync(command.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.User.EmailAlreadyExists, result.FirstError());
        _userPersistenceRepositoryMock.Verify(repo => repo.Insert(It.IsAny<User>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldCreateUser_WhenCommandIsValid()
    {
        // Arrange
        var address = new AddressCommandModel
        {
            Street = "123 Main St",
            City = "Anytown",
            District = "Downtown",
            Number = 1,
            State = "CA",
            Country = "USA",
            ZipCode = "99888-777"
        };
        var command = new CreateUserCommand("John", "Doe", "john.doe@example.com", "@Password123", DateTime.Now, address);

        _userReadOnlyRepositoryMock
            .Setup(repo => repo.EmailExistsAsync(command.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _passwordHasherMock
            .Setup(hasher => hasher.HashPassword(It.IsAny<string>()))
            .Returns("hashedPassword");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        _userPersistenceRepositoryMock.Verify(repo => repo.Insert(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
