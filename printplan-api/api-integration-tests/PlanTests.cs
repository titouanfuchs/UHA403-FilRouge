using System.Net;
using System.Net.Http.Json;
using printplan_api.Models.DTO;
using printplan_api.Models.DTO.Spools;
using printplan_api.Models.Enums;

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
    public async Task BPlanCanBePrinted()
    {
        await _httpClient.PostAsJsonAsync("/Spools", new PostSpoolDto()
        {
            Lenght = 100,
            Name = "Mediuam",
            Quantity = 1
        });
        PostPrintPlanDto query = new()
        {
            PrinterId = 1,
            PrintModelId = 1,
            Quantity = 1
        };

        var response = await _httpClient.PostAsJsonAsync("/Plan", query);
        string resultString = await response.Content.ReadAsStringAsync();
        
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
    
    [Fact]
    public async Task CPlanCanBePrintedButWithSpoolReplacement()
    {
        PostPrintPlanDto query = new()
        {
            PrinterId = 1,
            PrintModelId = 3,
            Quantity = 100000
        };

        var response = await _httpClient.PostAsJsonAsync("/Plan", query);
        PrintPlanDto result = await response.Content.ReadFromJsonAsync<PrintPlanDto>();
        
        Assert.True(result.SpoolReplacementEvents.Count > 1);
    }
    
    [Fact]
    public async Task DPlanCanBePrintedButNotEntirely()
    {
        PostPrintPlanDto query = new()
        {
            PrinterId = 1,
            PrintModelId = 3,
            Quantity = 100000
        };

        var response = await _httpClient.PostAsJsonAsync("/Plan", query);
        PrintPlanDto result = await response.Content.ReadFromJsonAsync<PrintPlanDto>();
        
        Assert.True(result.PrintQuantity < query.Quantity);
    }
    
    [Fact]
    public async Task EPlanCannotBePrinted_NoSpools()
    {
        await _httpClient.DeleteAsync("/Spools");
        
        PostPrintPlanDto query = new()
        {
            PrinterId = 1,
            PrintModelId = 1,
            Quantity = 1
        };

        var response = await _httpClient.PostAsJsonAsync("/Plan", query);
        BaseResponse errorResult = await response.Content.ReadFromJsonAsync<BaseResponse>();
        
        Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        Assert.True(errorResult.Status == Status.NoSpools);
        
        await _httpClient.PostAsync("/Spools/Reset", null);
    }
    
    [Fact]
    public async Task FPlanCannotBePrinted_PrinterDoesNotExists()
    {
       
        PostPrintPlanDto query = new()
        {
            PrinterId = 100,
            PrintModelId = 1,
            Quantity = 1
        };

        var response = await _httpClient.PostAsJsonAsync("/Plan", query);
        BaseResponse errorResult = await response.Content.ReadFromJsonAsync<BaseResponse>();
        
        Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        Assert.True(errorResult.Status == Status.NoPrinter);
    }
    
    [Fact]
    public async Task GPlanCannotBePrinted_ModelDoesNotExists()
    {
        PostPrintPlanDto query = new()
        {
            PrinterId = 1,
            PrintModelId = 100,
            Quantity = 1
        };

        var response = await _httpClient.PostAsJsonAsync("/Plan", query);
        BaseResponse errorResult = await response.Content.ReadFromJsonAsync<BaseResponse>();
        
        Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        Assert.True(errorResult.Status == Status.NoModels);
    }
    
    [Fact]
    public async Task HPlanCannotBePrinted_NotEnoughFilament()
    {
        await _httpClient.DeleteAsync("/Spools");

        await _httpClient.PostAsJsonAsync("/Spools", new PostSpoolDto()
        {
            Lenght = 2,
            Name = "Short",
            Quantity = 1
        });
        
        PostPrintPlanDto query = new()
        {
            PrinterId = 1,
            PrintModelId = 1,
            Quantity = 1
        };

        var response = await _httpClient.PostAsJsonAsync("/Plan", query);
        BaseResponse errorResult = await response.Content.ReadFromJsonAsync<BaseResponse>();
        
        Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        Assert.True(errorResult.Status == Status.NotEnoughFilament);
        
        await _httpClient.PostAsync("/Spools/Reset", null);
    }
}