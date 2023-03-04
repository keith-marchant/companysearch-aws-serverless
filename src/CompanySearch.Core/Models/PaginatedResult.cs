namespace CompanySearch.Core.Models;

public class PaginatedResult<T> where T : class
{
    public int TotalResults { get; set; }
    public int StartIndex { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<T> Results { get; set; } = new List<T>();

}
