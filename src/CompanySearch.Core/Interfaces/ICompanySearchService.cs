using CompanySearch.Core.Models;

namespace CompanySearch.Core.Interfaces
{
    public interface ICompanySearchService
    {
        Task<PaginatedResult<Company>> SearchAsync(string searchTerm, int startIndex, int pageSize, CancellationToken cancellationToken);
    }
}
