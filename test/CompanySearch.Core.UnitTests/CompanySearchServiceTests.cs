using CompanySearch.CompaniesHouse;
using CompanySearch.Core.Services;

namespace CompanySearch.Core.UnitTests;

public class CompanySearchServiceTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void SearchAsync_InvalidSearchTerm_ThrowsArgumentNullException(string searchTerm)
    {
        // Arrange
        Mock<CompanyHouseClient> mockClient = new Mock<CompanyHouseClient>();
        var searchService = new CompanySearchService(mockClient.Object);

        // Act
        var act = () => searchService.SearchAsync(searchTerm, 0, 100, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task SearchAsync_ResultsSmallerThanPageSize_TotalResultsMatchesResultsSize()
    {
        // Arrange
        Mock<CompanyHouseClient> mockClient = new Mock<CompanyHouseClient>();
        mockClient.Setup(m => m.CompaniesAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>()))
            .ReturnsAsync(new CompaniesHouse.CompanySearch { Items_per_page = 1, Start_index = 0, Total_results = 1, Items = new List<Items2>
            {
                new Items2
                {
                    Company_number = "1234",
                    Company_status = Items2Company_status.Active,
                }
            }});
        var searchService = new CompanySearchService(mockClient.Object);

        // Act
        var results = await searchService.SearchAsync("company123", 0, 100, CancellationToken.None);

        // Assert
        results.Results.Should().HaveCount(1);
        results.TotalResults.Should().Be(1);
        results.PageSize.Should().Be(100);
    }
}