namespace Base.Helpers;

public class PagedData<TEntity>
{
    public IEnumerable<TEntity> Items { get; set; } = default!;
    public int TotalCount { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}