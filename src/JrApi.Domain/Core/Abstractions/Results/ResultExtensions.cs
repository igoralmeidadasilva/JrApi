namespace JrApi.Domain.Core.Abstractions.Results;

public static class ResultExtensions
{
    public static Result<TValue> ToResultWithValue<TValue>(this Result result)
    {
        return result as Result<TValue> ?? throw new ResultConvertionException();
    }

    public static Result ToResultWithoutValue(this Result<object> result)
    {
        return result as Result ?? throw new ResultConvertionException();
    }

    public static IEnumerable<Error> GetErrorsByCode(this Result result, string codeStartPrefix)
    {
        IEnumerable<Error> newErrors = result.Errors
                .Where(error => error.Code.StartsWith(codeStartPrefix)).ToList();

        return newErrors;
    }

    public static IEnumerable<string> ExtractErrorsMessages(this IEnumerable<Error> errors)
    {
        IEnumerable<string> messages = errors
                .Select(error => error.Message);

        return messages;
    }
}
