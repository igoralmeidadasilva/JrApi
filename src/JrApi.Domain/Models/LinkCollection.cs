namespace JrApi.Domain.Models;

public class LinkCollection<T> where T: class 
{
    public T Value { get; set; }
    public List<Link> Links { get; set;} = [];

    public LinkCollection(T item) : base()
    {
        Value = item;
        Links = [];
    }
}