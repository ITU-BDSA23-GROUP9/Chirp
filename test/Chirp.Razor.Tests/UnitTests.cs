using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.Tests;

public class UnitTests
{
    [Fact]
    public async void CheepRepositoryGetAllCheepsTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var repository = new CheepRepository(context);
        var authorGuid = Guid.NewGuid();
        context.Cheeps.Add(new Cheep() { CheepId = 1, AuthorId = authorGuid, Author = new Author() { AuthorId = authorGuid, Name = "Anton", Email = "anlf@itu.dk" }, Text = "Hej, velkommen til kurset.", TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
        context.SaveChanges();

        // Act
        var result = await repository.GetAllCheeps();

        // Assert
        Assert.Equal("Anton", result[0].author);
        Assert.Equal("Hej, velkommen til kurset.", result[0].message);
        Assert.Equal("2023-08-01 13:08:28", result[0].timestamp);
    }

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
        var authorGuid = Guid.NewGuid();
        context.Cheeps.Add(new Cheep() { CheepId = 1, AuthorId = authorGuid, Author = new Author() { AuthorId = authorGuid, Name = "Anton", Email = "anlf@itu.dk" }, Text = "Hej, velkommen til kurset.", TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
        context.SaveChanges();

        // Act
        var result = await repository.GetCheeps(1, 1);

        // Assert
        Assert.Equal("Anton", result[0].author);
        Assert.Equal("Hej, velkommen til kurset.", result[0].message);
        Assert.Equal("2023-08-01 13:08:28", result[0].timestamp);
    }

    [Fact]
    public async void CheepRepositoryGetCheepsFromAuthorTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var repository = new CheepRepository(context);
        var authorGuid = Guid.NewGuid();
        context.Cheeps.Add(new Cheep() { CheepId = 1, AuthorId = authorGuid, Author = new Author() { AuthorId = authorGuid, Name = "Anton", Email = "anlf@itu.dk" }, Text = "Hej, velkommen til kurset.", TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
        context.SaveChanges();

        // Act
        var result = await repository.GetCheepsFromAuthor("Anton", 1, 1);

        // Assert
        Assert.Equal("Anton", result[0].author);
        Assert.Equal("Hej, velkommen til kurset.", result[0].message);
        Assert.Equal("2023-08-01 13:08:28", result[0].timestamp);
    }

    [Fact]
    public async void CheepRepositoryGetTotalCheepCountTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var repository = new CheepRepository(context);
        var authorGuid = Guid.NewGuid();
        context.Cheeps.Add(new Cheep() { CheepId = 1, AuthorId = authorGuid, Author = new Author() { AuthorId = authorGuid, Name = "Anton", Email = "anlf@itu.dk" }, Text = "Hej, velkommen til kurset.", TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
        context.SaveChanges();

        // Act
        var result = await repository.GetTotalCheepCount();

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async void CheepRepositoryGetTotalCheepCountFromAuthorTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var repository = new CheepRepository(context);
        var authorGuid = Guid.NewGuid();
        context.Cheeps.Add(new Cheep() { CheepId = 1, AuthorId = authorGuid, Author = new Author() { AuthorId = authorGuid, Name = "Anton", Email = "anlf@itu.dk" }, Text = "Hej, velkommen til kurset.", TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
        context.SaveChanges();

        // Act
        var result = await repository.GetTotalCheepCountFromAuthor("Anton");

        // Assert
        Assert.Equal(1, result);
    }
}