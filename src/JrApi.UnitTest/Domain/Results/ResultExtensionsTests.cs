using JrApi.Domain.Core.Abstractions.Results;

namespace JrApi.UnitTest.Domain.Results;

public class ResultExtensionsTests
{
    [Fact]
    public void GetErrorsByCode_WithMatchingPrefix_ShouldReturnFilteredErrors()
    {
        // Arrange
        var result = new Result
        {
            Errors =
            [
                Error.Create("Validation.Tests", "Error 1"),
                Error.Create("Validation.Tests2", "Error 2"),
                Error.Create("Error.Tests", "Error 3")
            ]
        };
        string codeStartPrefix = "Validation";

        // Act
        var filteredErrors = result.GetErrorsByCode(codeStartPrefix);

        // Assert
        Assert.NotNull(filteredErrors);
        Assert.Equal(2, filteredErrors.Count());
        Assert.All(filteredErrors, error => Assert.StartsWith(codeStartPrefix, error.Code));
    }

    [Fact]
    public void GetErrorsByCode_WithNonMatchingPrefix_ShouldReturnEmptyList()
    {
        // Arrange
        var result = new Result
        {
            Errors =
            [
                Error.Create("Validation.Tests", "Error 1"),
                Error.Create("Validation.Tests2", "Error 2"),
                Error.Create("Error.Tests", "Error 3")
            ]
        };
        string codeStartPrefix = "NOT_FOUND";

        // Act
        var filteredErrors = result.GetErrorsByCode(codeStartPrefix);

        // Assert
        Assert.Empty(filteredErrors);
    }

    [Fact]
    public void ExtractErrorsMessages_WithValidErrors_ShouldReturnMessagesList()
    {
        // Arrange
        var errors = new List<Error>
        {
            Error.Create("Validation.Tests", "Error 1"),
            Error.Create("Validation.Tests2", "Error 2"),
            Error.Create("Error.Tests", "Error 3")
        };

        // Act
        IEnumerable<string> errorMessages = errors.ExtractErrorsMessages();

        // Assert
        Assert.NotNull(errorMessages);
        Assert.Equal(3, errorMessages.Count());
        Assert.Contains("Error 1", errorMessages);
        Assert.Contains("Error 2", errorMessages);
        Assert.Contains("Error 3", errorMessages);
    }

    [Fact]
    public void ExtractErrorsMessages_WithEmptyErrors_ShouldReturnEmptyList()
    {
        // Arrange
        var errors = new List<Error>();

        // Act
        var errorMessages = errors.ExtractErrorsMessages();

        // Assert
        Assert.Empty(errorMessages);
    }
}
