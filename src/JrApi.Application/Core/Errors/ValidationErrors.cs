using JrApi.Domain.Core.Abstractions.Results;
using static JrApi.Domain.Constants.Constraints;

namespace JrApi.Application.Core.Errors;

public static class ValidationErrors
{
    public static class CreateUserErros
    {
        public static Error FirsNameIsRequired 
            => Error.Create("CreateUser.FirstName.IsRequired", "User's first name is required.");
        public static Error FirsNameMaxSize
            => Error.Create("CreateUser.FirstName.MaxSize", $"User's first name cannot be greater than {User.FIRST_NAME_MAX_SIZE}.");
    }

    private static string BuilderCode(string entityName, string property, string failure)
    {
        return $"{entityName}.{property}.{failure}";
    }
}
