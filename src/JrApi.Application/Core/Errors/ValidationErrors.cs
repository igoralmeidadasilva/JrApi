using JrApi.SharedKernel.Results;

namespace JrApi.Application.Core.Errors;

public static class ValidationErrors
{
    internal static class GenericErrorsMessages
    {
        internal static string IsRequired(string entity, string property)
            => $"{entity} {property} is required.";

        internal static string MaxSize(string entity, string property, int size)
            => $"{entity} {property} cannot be greater than {size}.";

        internal static string MinSize(string entity, string property, int size)
            => $"{entity} {property} cannot be less than {size}.";

        internal static string Format(string entity, string property)
            => $"{entity} {property} format is not valid.";
        internal static string Format(string entity, string property, string mask)
            => $"{entity} {property} format is not valid ({mask}).";
    }
    
    internal static class GenericEntityErrors
    {
        internal static Error IdIsRequired(string request, string entity)
            => Error.Create($"{request}.Id.IsRequired", GenericErrorsMessages.IsRequired(entity, "Id"), EErrorType.Validation);
    }
}