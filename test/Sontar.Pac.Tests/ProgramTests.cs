using Microsoft.AspNetCore.Mvc.Testing;

namespace Sontar.Pac;

/// <summary>
/// Integration tests.
/// </summary>
public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public ProgramTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Fact]
    public async Task Get_Root_Returns_hello_world()
    {
        var client = _webApplicationFactory.CreateClient();
        var response = await client.GetAsync("/");

        var mediaType = response.Content.Headers.ContentType?.MediaType;
        mediaType.Should().Be("text/plain");

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be("Hello World!");
    }
}
