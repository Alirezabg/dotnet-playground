using System.Net.Http.Json;
using Catalogue.Api.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Catalogue.Tests.Api;

// 📌 YOUR TURN — this is the practice test. Complete it yourself.
//
// Goal: prove that GET /products returns every product that was created.
//
// Steps:
//   1. Arrange: POST two products with different names via _client.
//   2. Act:     GET /products and read the response into ProductResponse[].
//   3. Assert:  both created ids are present in the returned collection.
//
// When you're done: remove the Skip and run `dotnet test`.
// Ask your coach: "Which V Model level is this test, and why?"
public class GetAllProductsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public GetAllProductsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact(Skip = "TODO: implement this test as part of your practice.")]
    public async Task Get_ReturnsAllCreatedProducts()
    {
        await Task.CompletedTask;
        throw new NotImplementedException("Write the Arrange/Act/Assert steps described above.");
    }
}
