using JrApi.Domain.Core.Abstractions.Results;

namespace JrApi.UnitTest.Domain.Results;

public class ResultTests
{
    [Fact]
    public void Success_ShouldReturnResultWithIsSuccessTrue()
    {
        // Act
        var result = Result.Success();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Failure_WithSingleError_ShouldReturnResultWithIsSuccessFalse()
    {
        // Arrange
        var error = Error.Create("Error.Tests", "Error 1");

        // Act
        var result = Result.Failure(error);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal(error, result.Errors.First());
    }

    [Fact]
    public void Failure_WithMultipleErrors_ShouldReturnResultWithIsSuccessFalse()
    {
        // Arrange
        var errors = new List<Error>
        {
            Error.Create("Error.Tests", "Error 1"),
            Error.Create("Error.Tests", "Error 2")
        };

        // Act
        var result = Result.Failure(errors);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(2, result.Errors.Count);
    }

    [Fact]
    public void Success_WithGenericType_ShouldReturnResultWithValueAndIsSuccessTrue()
    {
        // Arrange
        var value = "SuccessValue";

        // Act
        var result = Result.Success(value);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.Value);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Failure_WithGenericTypeAndSingleError_ShouldReturnResultWithIsSuccessFalse()
    {
        // Arrange
        var error = Error.Create("Error.Tests", "Error 1");

        // Act
        var result = Result.Failure<string>(error);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Single(result.Errors);
        Assert.Equal(error, result.Errors.First());
    }

    [Fact]
    public void Failure_WithGenericTypeAndMultipleErrors_ShouldReturnResultWithIsSuccessFalse()
    {
        // Arrange
        var errors = new List<Error>
        {
            Error.Create("Error.Tests", "Error 1"),
            Error.Create("Error.Tests", "Error 2")
        };

        // Act
        var result = Result.Failure<string>(errors);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Equal(2, result.Errors.Count);
    }

    [Fact]
    public void FirstError_ShouldReturnTheFirstError()
    {
        // Arrange
        var errors = new List<Error>
        {
            Error.Create("Error.Tests", "Error 1"),
            Error.Create("Error.Tests", "Error 2")
        };
        var result = Result.Failure(errors);

        // Act
        var firstError = result.FirstError();

        // Assert
        Assert.Equal(errors[0], firstError);
    }

    [Fact]
    public void HasError_ShouldReturnTrueIfThereAreErrors()
    {
        // Arrange
        var error = Error.Create("Error.Tests", "Error 1");
        var result = Result.Failure(error);

        // Act
        var hasError = result.HasError();

        // Assert
        Assert.True(hasError);
    }

    [Fact]
    public void HasError_ShouldReturnFalseIfNoErrors()
    {
        // Act
        var result = Result.Success();

        // Assert
        Assert.False(result.HasError());
    }

    [Fact]
    public void HasManyErrors_ShouldReturnTrueIfMoreThanOneError()
    {
        // Arrange
        var errors = new List<Error>
        {
            Error.Create("Error.Tests", "Error 1"),
            Error.Create("Error.Tests", "Error 2")
        };
        var result = Result.Failure(errors);

        // Act
        var hasManyErrors = result.HasManyErrors();

        // Assert
        Assert.True(hasManyErrors);
    }

    [Fact]
    public void HasManyErrors_ShouldReturnFalseIfOneOrNoError()
    {
        // Arrange
        var error = Error.Create("Error.Tests", "Error 1");
        var result = Result.Failure(error);

        // Act
        var hasManyErrors = result.HasManyErrors();

        // Assert
        Assert.False(hasManyErrors);
    }

    [Fact]
    public void HasOneError_ShouldReturnTrueIfThereIsExactlyOneError()
    {
        // Arrange
        var error = Error.Create("Error.Tests", "Error 1");
        var result = Result.Failure(error);

        // Act
        var hasOneError = result.HasOneError();

        // Assert
        Assert.True(hasOneError);
    }

    [Fact]
    public void ErrorOrSuccess_ShouldReturnTrueIfThereAreNoErrors()
    {
        // Act
        var result = Result.Success();

        // Assert
        Assert.True(result.ErrorOrSucces());
    }

    [Fact]
    public void ErrorOrSuccess_ShouldReturnFalseIfThereAreErrors()
    {
        // Arrange
        var error = Error.Create("Error.Tests", "Error 1");
        var result = Result.Failure(error);

        // Act
        var errorOrSuccess = result.ErrorOrSucces();

        // Assert
        Assert.False(errorOrSuccess);
    }
}
