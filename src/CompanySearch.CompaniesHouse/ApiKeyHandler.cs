using System.Text;

namespace CompanySearch.CompaniesHouse;

public class ApiKeyHandler : DelegatingHandler
{
    private readonly string? _apiKey;

    public ApiKeyHandler(string? apiKey)
    {
        _apiKey = apiKey;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(_apiKey))
        {
            request.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_apiKey + ":"))}");
        }
        return await base.SendAsync(request, cancellationToken);
    }
}
