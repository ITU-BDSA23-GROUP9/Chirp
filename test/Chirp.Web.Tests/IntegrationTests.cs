namespace Chirp.Web.Tests;
using Microsoft.AspNetCore.Mvc.Testing;
using HtmlAgilityPack;


//Code taken from lecture-slides-05 and small parts adapted by: Oline <okre@itu.dk>, Anton <anlf@itu.dk> & Clara <clwj@itu.dk>
public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _fixture;
    private readonly HttpClient _client;

    public IntegrationTests(WebApplicationFactory<Program> fixture)
    {
        _fixture = fixture;
        _client = _fixture.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = true, HandleCookies = true });
    }

    [Fact]
    public async void CanSeePublicTimeline()
    {
        // Arrange
        var response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();

        // Act
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Contains("Chirp!", content);
        Assert.Contains("Public Timeline", content);
    }

    [Theory]
    [InlineData("Helge")]
    [InlineData("Rasmus")]
    public async void CanSeePrivateTimeline(string author)
    {   
        // Arrange
        var response = await _client.GetAsync($"/{author}");
        response.EnsureSuccessStatusCode();

        // Act
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Contains("Chirp!", content);
        Assert.Contains($"{author}'s Timeline", content);
    }

    [Fact]
    public async void PageContainsMax32Cheeps() 
    {
        // Arrange
        var response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        // Act
        int cheepCount = content.Split("cheeps").Length;
        var parser = ;

        // Assert
        Assert.Equal(32, cheepCount);

    }
}