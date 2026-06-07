using System.Net;
using System.Net.Http.Json;
using Catalogue.Api.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Catalogue.Tests.Api;

// INTEGRATION tests (V Model): the endpoint through the real pipeline, hosted in-memory.
public class ProductsEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductsEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_ThenGet_ReturnsCreatedProduct()
    {
        var request = new CreateProductRequest("Treatment Couch", 1999.99m, "EUR");

        var postResponse = await _client.PostAsJsonAsync("/products", request);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<ProductResponse>();
        Assert.NotNull(created);

        var getResponse = await _client.GetAsync($"/products/{created!.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var fetched = await getResponse.Content.ReadFromJsonAsync<ProductResponse>();
        Assert.Equal(created.Id, fetched!.Id);
        Assert.Equal("Treatment Couch", fetched.Name);
    }

    [Fact]
    public async Task Get_UnknownId_Returns404()
    {
        var response = await _client.GetAsync($"/products/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_NegativePrice_Returns400()
    {
        var request = new CreateProductRequest("Bad Product", -5m, "EUR");

        var response = await _client.PostAsJsonAsync("/products", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
