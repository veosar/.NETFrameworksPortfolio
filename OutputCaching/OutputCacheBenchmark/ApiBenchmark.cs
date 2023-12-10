using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using Bogus;

namespace OutputCacheBenchmark;
public class ApiBenchmark
{
    private HttpClient _client;
    private Faker<Customer> _customerFaker;

    [GlobalSetup]
    public void Setup()
    {
        _client = new HttpClient();
        _customerFaker = new Faker<Customer>()
            .UseSeed(12345)
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
            .RuleFor(c => c.DateOfBirth, f => DateOnly.FromDateTime(f.Date.Past(30)));
    }

    [Benchmark]
    public async Task GetCustomersWithCache()
    {
        var response = await _client.GetAsync("http://localhost:8080/customers");
        response.EnsureSuccessStatusCode();
    }

    [Benchmark]
    public async Task GetCustomersWithoutCache()
    {
        var response = await _client.GetAsync("http://localhost:8080/customersnocache");
        response.EnsureSuccessStatusCode();
    }
    
    [Benchmark]
    public async Task GetCustomersWithCacheFollowedByPost()
    {
        var response = await _client.GetAsync("http://localhost:8080/customers");
        response.EnsureSuccessStatusCode();
        
        response = await _client.GetAsync("http://localhost:8080/customers");
        response.EnsureSuccessStatusCode();
        
        response = await _client.GetAsync("http://localhost:8080/customers");
        response.EnsureSuccessStatusCode();

        var customer = _customerFaker.Generate();
        
        using var jsonContent = new StringContent(
            JsonSerializer.Serialize(customer),
            Encoding.UTF8,
            "application/json");

        response = await _client.PostAsync("http://localhost:8080/customers", jsonContent);
        response.EnsureSuccessStatusCode();
        
        response = await _client.GetAsync("http://localhost:8080/customers");
        response.EnsureSuccessStatusCode();
        
        response = await _client.GetAsync("http://localhost:8080/customers");
        response.EnsureSuccessStatusCode();
        
        response = await _client.GetAsync("http://localhost:8080/customers");
        response.EnsureSuccessStatusCode();
    }

    [Benchmark]
    public async Task GetCustomersWithoutCacheFollowedByPost()
    {
        var response = await _client.GetAsync("http://localhost:8080/customersnocache");
        response.EnsureSuccessStatusCode();
        
        response = await _client.GetAsync("http://localhost:8080/customersnocache");
        response.EnsureSuccessStatusCode();
        
        response = await _client.GetAsync("http://localhost:8080/customersnocache");
        response.EnsureSuccessStatusCode();

        var customer = _customerFaker.Generate();

        using var jsonContent = new StringContent(
            JsonSerializer.Serialize(customer),
            Encoding.UTF8,
            "application/json");

        response = await _client.PostAsync("http://localhost:8080/customers", jsonContent);
        response.EnsureSuccessStatusCode();
        
        response = await _client.GetAsync("http://localhost:8080/customersnocache");
        response.EnsureSuccessStatusCode();
        
        response = await _client.GetAsync("http://localhost:8080/customersnocache");
        response.EnsureSuccessStatusCode();
        
        response = await _client.GetAsync("http://localhost:8080/customersnocache");
        response.EnsureSuccessStatusCode();
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _client.Dispose();
    }
}