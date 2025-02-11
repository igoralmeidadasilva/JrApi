namespace JrApi.SharedKernel.Results;

public static class ResultExtensions
{
    public static Result<TValue> ToResultWithValue<TValue>(this Result result)
        => result as Result<TValue> ?? throw new ResultConvertionException();
    public static IEnumerable<Error> GetErrorsByCode(this Result result, string codeStartPrefix)
        => result.Errors.Where(error => error.Code.StartsWith(codeStartPrefix)).ToList();
    public static IEnumerable<string> ExtractErrorsMessages(this IEnumerable<Error> errors)
        => errors.Select(error => error.Message);
}