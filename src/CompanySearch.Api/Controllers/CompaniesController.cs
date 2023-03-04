using CompanySearch.Core.Interfaces;
using CompanySearch.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanySearch.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CompaniesController : Controller
{
    private readonly ILogger<CalculatorController> _logger;
    private readonly ICompanySearchService _companySearchService;

    public CompaniesController(ILogger<CalculatorController> logger, ICompanySearchService companySearchService)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(companySearchService, nameof(companySearchService));
        _logger = logger;
        _companySearchService = companySearchService;
    }

    /// <summary>
    /// Search for a company given a provided search term.
    /// </summary>
    /// <param name="searchTerm">String used to identify a company. e.g. Name, or Company Number</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A paginated list of company search results.</returns>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<Company>>> Search([FromQuery(Name = "q")] string searchTerm, CancellationToken cancellationToken)
    {
        var results = await _companySearchService.SearchAsync(searchTerm, 0, 100, cancellationToken);
        return Ok(results);
    }
}
