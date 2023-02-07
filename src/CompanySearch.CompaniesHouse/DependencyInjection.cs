using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace CompanySearch.CompaniesHouse
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCompanyHouseClient(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetRequiredSection("companiesHouse")
                                        .Get<CompaniesHouseSettings>();

            if (settings is null)
            {
                throw new NullReferenceException(nameof(settings));
            }

            services
               .AddHttpClient<CompanyHouseClient>(nameof(CompanyHouseClient), (client) => new CompanyHouseClient(client) { BaseUrl = settings.Uri })
               .AddApiKeyHandler(settings.ApiKey)
               .SetHandlerLifetime(TimeSpan.FromMinutes(10))
               .AddPolicyHandler(GetRetryPolicy());

            return services;
        }

        public static IHttpClientBuilder AddApiKeyHandler(this IHttpClientBuilder httpClientBuilder, string? apiKey) =>
            httpClientBuilder.AddHttpMessageHandler(() => new ApiKeyHandler(apiKey));

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
           HttpPolicyExtensions
               .HandleTransientHttpError()
               .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
