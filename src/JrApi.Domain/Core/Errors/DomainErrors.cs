using JrApi.SharedKernel.Results;

namespace JrApi.Domain.Core.Errors;

public static class DomainErrors
{
    private static class General
    {
        internal static string NotFound(string entity, string property)
            => $"{entity} {property} cannot be found.";
    }

    public static class User
    {
        private const string ENTITY = nameof(Entities.Users.User); 
        public static Error EmailAlreadyExists
            => Error.Create("UserError.Email.AlreadyExists", "User Email already exists.", EErrorType.Conflict);

        public static Error IdNotFound
            => Error.Create("UserError.Id.NotFound", General.NotFound(ENTITY, "Id"), EErrorType.NotFound);

        public static Error NoneCanBeFound
            => Error.Create("UserError.NoneCanBeFound", "No users could be found", EErrorType.NotFound);
    }
}