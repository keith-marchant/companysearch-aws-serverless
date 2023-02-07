using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace CompanySearch.CompaniesHouse.IntegrationTests;

public class CompanyHouseClientTests : IDisposable
{
    private readonly ITestOutputHelper _output;
    private readonly ServiceProvider _serviceProvider;

    public CompanyHouseClientTests(ITestOutputHelper output)
    {
        _output = output;

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets<CompanyHouseClientTests>()
            .AddEnvironmentVariables()
            .Build();

        _serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)
            .AddCompanyHouseClient(configuration)
            .BuildServiceProvider();
    }

    [Fact]
    public async Task SearchAsync_ExistingCompany_ReturnsPopulatedResult()
    {
        // Arrange
        await using var scope = _serviceProvider.CreateAsyncScope();
        var client = scope.ServiceProvider.GetRequiredService<CompanyHouseClient>();

        // Act
        var result = await client.SearchAsync("InfoTrack", 10, 0, CancellationToken.None);

        // Visualise
        _output.WriteLine(JsonConvert.SerializeObject(result));

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCountGreaterThan(0);
    }

    public void Dispose()
    {
        _serviceProvider.Dispose();
        GC.SuppressFinalize(this);
    }
}