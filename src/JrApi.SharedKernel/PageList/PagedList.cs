namespace JrApi.SharedKernel;

public class PagedList<T>  : List<T> where T : class
{
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;

    public PagedList(List<T> items, int totalCount, int pageNumber, int pageSize)
    {
        AddRange(items);

        PageNumber = pageNumber;
        TotalCount = totalCount;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalCount/(double)pageSize);
    }
}