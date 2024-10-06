using JrApi.Domain.Core.Abstractions.Results;

namespace JrApi.UnitTest.Domain.Results;

public class ResultTTests
{
    [Fact]
    public void Success_ShouldReturnResultWithIsSuccessTrueAndValue()
    {
        // Arrange
        var expectedValue = "TestValue";

        // Act
        var result = Result<string>.Success(expectedValue);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(expectedValue, result.Value);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Failure_WithSingleError_ShouldReturnResultWithIsSuccessFalseAndDefaultTValue()
    {
        // Arrange
        var error = Error.Create("Error.Tests", "Error 1");

        // Act
        var result = Result<string>.Failure(error);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Null(result.Value); 
        Assert.Single(result.Errors);
        Assert.Equal(error, result.Errors.First());
    }

    [Fact]
    public void Failure_WithMultipleErrors_ShouldReturnResultWithIsSuccessFalseAndDefaultTValue()
    {
        // Arrange
        var errors = new List<Error>
        {
            Error.Create("Error.Tests", "Error 1"),
            Error.Create("Error.Tests", "Error 2")
        };

        // Act
        var result = Result<string>.Failure(errors);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Null(result.Value); 
        Assert.Equal(2, result.Errors.Count);
    }

    [Fact]
    public void ImplicitOperator_ShouldReturnSuccessResultWithValue()
    {
        // Arrange
        var expectedValue = "TestValue";

        // Act
        Result<string> result = expectedValue;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedValue, result.Value);
        Assert.Empty(result.Errors);
    }
}
