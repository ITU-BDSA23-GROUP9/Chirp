namespace Chirp.Web.Tests;
using Microsoft.AspNetCore.Mvc.Testing;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;


//Code taken from lecture-slides-05 and small parts adapted by: Oline <okre@itu.dk>, Anton <anlf@itu.dk> & Clara <clwj@itu.dk>
public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _fixture;
    private readonly HttpClient _client;

    public IntegrationTests(WebApplicationFactory<Program> fixture)
    {
        _fixture = fixture;
        _client = _fixture.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = true, HandleCookies = true });
        Environment.SetEnvironmentVariable("GITHUB_CLIENT_ID", "YEHA");
        Environment.SetEnvironmentVariable("GITHUB_CLIENT_SECRET", "NO?");
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
    public async void CanSeeUserTimeline(string author)
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
        int cheepCount = 0;

        int listStart = content.IndexOf("<ul id=\"messagelist\" class=\"cheeps\">");
        if (listStart >= 0)
        {
            int listEnd = content.IndexOf("</ul>", listStart);

            if (listEnd >= 0)
            {
                string listContent = content.Substring(listStart, listEnd - listStart);

                // Count the number of list items (list items are represented as "<li>")
                cheepCount = Regex.Matches(listContent, "<li>").Count;
            }
        }

        // Assert
        Assert.Equal(32, cheepCount);

    }

    [Fact]
    public async void HomePageIsEqualToPage1()
    {
        // Arrange
        var homePage = await _client.GetAsync("/");
        homePage.EnsureSuccessStatusCode();
        var HPContent = await homePage.Content.ReadAsStringAsync();

        var pageOne = await _client.GetAsync("/?pageNumber=1");
        pageOne.EnsureSuccessStatusCode();
        var pageOneContent = await pageOne.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HPContent, pageOneContent);

    }

    [Fact]
    public async void NewlyCreatedAuthorIsFollowingNone()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var cheepRepo = new CheepRepository(context);
        var authorRepo = new AuthorRepository(context);
        var author = new Author() { UserName = "Anna", Email = "anna@itu.dk" };
        context.Authors.Add(author);
        context.SaveChanges();

        // Act
        var result = authorRepo.FindAuthorByName("Anna");

        // Assert
        Assert.Equal(result.Following.Count == 0);
    }

    [Fact]
    public async void NewlyCreatedAuthorHasNoFollowers()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var cheepRepo = new CheepRepository(context);
        var authorRepo = new AuthorRepository(context);
        var author = new Author() { UserName = "Anna", Email = "anna@itu.dk" };
        context.Authors.Add(author);
        context.SaveChanges();

        // Act
        var result = authorRepo.FindAuthorByName("Anna");

        // Assert
        Assert.Equal(result.Followers.Count == 0);
    }
}