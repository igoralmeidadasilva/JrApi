namespace JrApi.Application.Dtos;

public record PagedResponseDto<T> where T : class
{
    public IEnumerable<T> Result { get; set; } = [];
    public int Count { get; set; }
    public string? Next { get; set; }
    public string? Previous { get; set; }
}