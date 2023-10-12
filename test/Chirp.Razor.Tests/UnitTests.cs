using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.Tests;

public class UnitTests
{
    [Fact]
    public async void CheepRepositoryGetCheepsTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var repository = new CheepRepository(context);

        context.Cheeps.Add(new Cheep() { CheepId = 1, AuthorId = 1, Author = new Author() { AuthorId = 1, Name = "Anton", Email = "anlf@itu.dk" }, Text = "Hej, velkommen til kurset.", TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
        context.SaveChanges();

        // Act
        var result = await repository.GetCheeps();

        // Assert
        Assert.Equal("Anton", result[0].author);
        Assert.Equal("Hej, velkommen til kurset.", result[0].message);
        Assert.Equal("2023-08-01 13:08:28", result[0].timestamp);
    }
}