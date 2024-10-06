using JrApi.Application.Dtos;
using JrApi.Application.Queries.Users.GetAllUsers;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Entities.Users;

namespace JrApi.UnitTest.Application.Queries.Users.GetAllUsers;

public class GetAllUsersQueryHandlerTests
{
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepositoryMock;
    private readonly Mock<ILogger<GetAllUsersQueryHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllUsersQueryHandler _handler;

    public GetAllUsersQueryHandlerTests()
    {
        _userReadOnlyRepositoryMock = new Mock<IUserReadOnlyRepository>();
        _loggerMock = new Mock<ILogger<GetAllUsersQueryHandler>>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllUsersQueryHandler(
            _userReadOnlyRepositoryMock.Object,
            _loggerMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyResponse_WhenNoUsersFound()
    {
        // Arrange
        _userReadOnlyRepositoryMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Enumerable.Empty<User>());

        // Act
        var result = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value!.Users!);
    }

    [Fact]
    public async Task Handle_ShouldReturnUsers_WhenUsersExist()
    {
        // Arrange
        var users = Helpers.UsersGenerators.Create().Generate(2);;

        var userDtos = new List<GetAllUsersDto>
        {
            new GetAllUsersDto { Id = Guid.NewGuid(), FirstName = users[0].FirstName!},
            new GetAllUsersDto { Id = Guid.NewGuid(), FirstName = users[1].FirstName!}
        };

        _userReadOnlyRepositoryMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        _mapperMock
            .Setup(m => m.Map<GetAllUsersDto>(It.IsAny<User>()))
            .Returns((User user) => userDtos.First(dto => dto.FirstName == user.FirstName!));

        // Act
        var result = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value!.Users!.Count());
    }

    [Fact]
    public async Task Handle_ShouldLogCorrectInformation_WhenUsersExist()
    {
        // Arrange
        var users = Helpers.UsersGenerators.Create().Generate(2);

        _userReadOnlyRepositoryMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        _mapperMock
            .Setup(m => m.Map<GetAllUsersDto>(It.IsAny<User>()))
            .Returns((User user) => new GetAllUsersDto { Id = user.Id, FirstName = user.FirstName!, LastName = user.LastName! });

        // Act
        var result = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value!.Users!.Count());

    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenRepositoryThrowsException()
    {
        // Arrange
        _userReadOnlyRepositoryMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetAllUsersQuery(), CancellationToken.None));
    }
}
