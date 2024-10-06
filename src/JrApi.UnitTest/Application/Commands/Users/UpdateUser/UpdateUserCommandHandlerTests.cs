using JrApi.Application.Commands.Users.CreateUser;
using JrApi.Application.Commands.Users.UpdateUser;
using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces;
using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Entities.Users;

namespace JrApi.UnitTest.Application.Commands.Users.UpdateUser;

public class UpdateUserCommandHandlerTests
{
    private readonly Mock<ILogger<CreateUserCommandHandler>> _loggerMock;
    private readonly Mock<IUserPersistenceRepository> _userPersistenceRepositoryMock;
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateUserCommandHandler _handler;

    public UpdateUserCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreateUserCommandHandler>>();
        _userPersistenceRepositoryMock = new Mock<IUserPersistenceRepository>();
        _userReadOnlyRepositoryMock = new Mock<IUserReadOnlyRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new UpdateUserCommandHandler(
            _loggerMock.Object,
            _userPersistenceRepositoryMock.Object,
            _userReadOnlyRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
    {
        // Arrange
        var command = new UpdateUserCommand("John", "Doe", DateTime.Now, null!)
        {
            Id = Guid.NewGuid()
        };

        _userReadOnlyRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User)null!);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.User.IdNotFound, result.Errors.First());
    }

    [Fact]
    public async Task Handle_ShouldUpdateUser_WhenUserExists()
    {
        // Arrange
        var command = new UpdateUserCommand("John", "Doe", DateTime.Now, null!)
        {
            Id = Guid.NewGuid()
        };

        User existingUser = UsersGenerators.Create().Generate();
        User updatedUser = UsersGenerators.Create().Generate();

        _userReadOnlyRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);

        _mapperMock
            .Setup(x => x.Map<User>(command))
            .Returns(updatedUser);

        _userPersistenceRepositoryMock
            .Setup(x => x.Update(It.IsAny<User>()))
            .Verifiable();

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(Unit.Value, result.Value);

        _userPersistenceRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldMapUpdatedUserCorrectly()
    {
        // Arrange
        var command = new UpdateUserCommand("John", "Doe", DateTime.Now, null!)
        {
            Id = Guid.NewGuid()
        };

        User existingUser = UsersGenerators.Create().Generate();
        User updatedUser = UsersGenerators.Create().Generate();

        _userReadOnlyRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);

        _mapperMock
            .Setup(x => x.Map<User>(command))
            .Returns(updatedUser);

        _userPersistenceRepositoryMock
            .Setup(x => x.Update(It.IsAny<User>()))
            .Verifiable();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);

        _mapperMock.Verify(x => x.Map<User>(command), Times.Once);
        _userPersistenceRepositoryMock.Verify(x => x.Update(existingUser), Times.Once);
    }
}
