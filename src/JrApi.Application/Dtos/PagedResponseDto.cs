namespace JrApi.Application.Dtos;

public record PagedResponseDto<T>
{
    public IEnumerable<T> Result { get; set; } = [];
    public int Count { get; set; }
    public string? Next { get; set; }
    public string? Previous { get; set; }
}

// public class LinkHelper<T> where T: class 
// {
//     public T Value { get; set; }
//     public List<Link> Links { get; set;}
//     public LinkHelper()
//     {
//         Links = new List<Link>();
//     }
//     public LinkHelper(T item) : base()
//     {
//         Value = item;
//         Links = new List<Link>();
//     }
// }