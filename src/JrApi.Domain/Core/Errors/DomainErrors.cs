using JrApi.Domain.Core.Abstractions.Results;

namespace JrApi.Domain.Core.Errors;

public static class DomainErrors
{
    public static class General
    {

    }

    public static class User
    {
        public static Error EmailAlreadyExists
            => Error.Create("UserError.Email.AlreadyExists", "User Email already exists", ErrorType.Conflict);
    }
}
