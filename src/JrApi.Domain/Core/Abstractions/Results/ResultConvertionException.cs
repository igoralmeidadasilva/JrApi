namespace JrApi.Domain.Core.Abstractions.Results;

public class ResultConvertionException : Exception
{
    private const string DEFAULT_MESSAGE = "Error converting value from Result to ResultT";
    public ResultConvertionException() : base(DEFAULT_MESSAGE)
    { }

    public ResultConvertionException(string message) : base(message)
    { }

    public ResultConvertionException(string message, Exception inner) : base(message)
    { }
}