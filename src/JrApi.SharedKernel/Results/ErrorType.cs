using System.Text.Json.Serialization;

namespace JrApi.SharedKernel.Results;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EErrorType
{
    None,
    Failure,
    Unexpected,
    Validation,
    Conflict,
    NotFound,
    Unauthorized,
    Forbidden
}