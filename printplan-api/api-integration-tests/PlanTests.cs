using System.Net;
using System.Net.Http.Json;
using printplan_api.Models.DTO;
namespace api_integration_tests;

public class PlanTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public PlanTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateDefaultClient();
    }

    [Fact]
    public async Task PlanCanBePrinted()
    {
        PostPrintPlanDto query = new()
        {
            PrinterId = 1,
            PrintModelId = 1,
            Quantity = 1
        };

        var response = await _httpClient.PostAsJsonAsync("/Plan", query);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}