namespace JrApi.SharedKernel.PageList;

public record PagedResponse<T>
{
    public IEnumerable<T> Items { get; set; } = [];
    public int Count { get; set; }
    public string Next { get; set; } = string.Empty;
    public string Previous { get; set; } = string.Empty;
}