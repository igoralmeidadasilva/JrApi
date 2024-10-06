using JrApi.Application.Dtos;
using JrApi.Application.Queries.Users.GetUserById;
using JrApi.Domain.Core.Errors;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Entities.Users;

namespace JrApi.UnitTest.Application.Queries.Users.GetUserById;

public class GetUserByIdQueryHandlerTests
{
    private readonly Mock<IUserReadOnlyRepository> _userRepositoryMock;
    private readonly Mock<ILogger<GetUserByIdQueryHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetUserByIdQueryHandler _handler;

    public GetUserByIdQueryHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserReadOnlyRepository>();
        _loggerMock = new Mock<ILogger<GetUserByIdQueryHandler>>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetUserByIdQueryHandler(
            _userRepositoryMock.Object,
            _loggerMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_UserExists_ReturnsSuccessResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = Helpers.UsersGenerators.Create().Generate();
        var request = new GetUserByIdQuery(userId);
        var mappedUser = new GetUserByIdDto{ Id = userId };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        _mapperMock.Setup(m => m.Map<GetUserByIdDto>(user))
            .Returns(mappedUser);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(mappedUser, result.Value!.User);

        _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_UserDoesNotExist_ReturnsFailureResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new GetUserByIdQuery(userId);

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User)null!);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.User.IdNotFound, result.Errors.First());

        _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
    }
}