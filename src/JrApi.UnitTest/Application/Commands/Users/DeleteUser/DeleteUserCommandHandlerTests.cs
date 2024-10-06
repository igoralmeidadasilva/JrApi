using JrApi.Application.Commands.Users.DeleteUser;
using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces;
using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;

namespace JrApi.UnitTest.Application.Commands.Users.DeleteUser;

public class DeleteUserCommandHandlerTests
{
    private readonly Mock<ILogger<DeleteUserCommandHandler>> _loggerMock;
    private readonly Mock<IUserPersistenceRepository> _userPersistenceRepositoryMock;
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly DeleteUserCommandHandler _handler;

    public DeleteUserCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<DeleteUserCommandHandler>>();
        _userPersistenceRepositoryMock = new Mock<IUserPersistenceRepository>();
        _userReadOnlyRepositoryMock = new Mock<IUserReadOnlyRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();

        _handler = new DeleteUserCommandHandler(
            _loggerMock.Object,
            _userPersistenceRepositoryMock.Object,
            _userReadOnlyRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserDoesNotExist()
    {
        // Arrange
        var command = new DeleteUserCommand(Guid.NewGuid());
        _userReadOnlyRepositoryMock.Setup(repo => repo.ExistsAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.User.IdNotFound, result.FirstError());

        _userPersistenceRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldDeleteUser_WhenUserExists()
    {
        // Arrange
        var command = new DeleteUserCommand(Guid.NewGuid());
        _userReadOnlyRepositoryMock.Setup(repo => repo.ExistsAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);

        _userPersistenceRepositoryMock.Verify(repo => repo.Delete(command.Id), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
