using JrApi.Domain.Core.Abstractions.Results;

namespace JrApi.Domain.Core.Errors;

public static class DomainErrors
{
    private static class GeneralErrors
    {
        internal static string NotFound(string entity, string property)
            => $"{entity} {property} cannot be found.";
    }

    public static class User
    {
        private const string ENTITY = nameof(Entities.Users.User); 
        public static Error EmailAlreadyExists
            => Error.Create("UserError.Email.AlreadyExists", "User Email already exists.", ErrorType.Conflict);

        public static Error IdNotFound
            => Error.Create("UserError.Id.NotFound", GeneralErrors.NotFound(ENTITY, "Id"), ErrorType.NotFound);
    }
}
