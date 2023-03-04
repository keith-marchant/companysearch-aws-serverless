using CompanySearch.CompaniesHouse;
using CompanySearch.Core.Interfaces;
using CompanySearch.Core.Models;

namespace CompanySearch.Core.Services
{
    internal class CompanySearchService : ICompanySearchService
    {
        private readonly CompanyHouseClient _companyHouseClient;

        public CompanySearchService(CompanyHouseClient companyHouseClient)
        {
            _companyHouseClient = companyHouseClient;
        }

        public Task<PaginatedResult<Company>> SearchAsync(string searchTerm, int startIndex, int pageSize, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
