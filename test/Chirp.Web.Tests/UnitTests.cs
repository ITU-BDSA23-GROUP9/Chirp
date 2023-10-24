using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Web.Tests;

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
        context.Cheeps.Add(new Cheep() { CheepId = Guid.NewGuid(), AuthorId = authorGuid, Author = new Author() { AuthorId = authorGuid, Name = "Anton", Email = "anlf@itu.dk" }, Text = "Hej, velkommen til kurset.", TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
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
        context.Cheeps.Add(new Cheep() { CheepId = Guid.NewGuid(), AuthorId = authorGuid, Author = new Author() { AuthorId = authorGuid, Name = "Anton", Email = "anlf@itu.dk" }, Text = "Hej, velkommen til kurset.", TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
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
        context.Cheeps.Add(new Cheep() { CheepId = Guid.NewGuid(), AuthorId = authorGuid, Author = new Author() { AuthorId = authorGuid, Name = "Anton", Email = "anlf@itu.dk" }, Text = "Hej, velkommen til kurset.", TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
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
        context.Cheeps.Add(new Cheep() { CheepId = Guid.NewGuid(), AuthorId = authorGuid, Author = new Author() { AuthorId = authorGuid, Name = "Anton", Email = "anlf@itu.dk" }, Text = "Hej, velkommen til kurset.", TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
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
        context.Cheeps.Add(new Cheep() { CheepId = Guid.NewGuid(), AuthorId = authorGuid, Author = new Author() { AuthorId = authorGuid, Name = "Anton", Email = "anlf@itu.dk" }, Text = "Hej, velkommen til kurset.", TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
        context.SaveChanges();

        // Act
        var result = await repository.GetTotalCheepCountFromAuthor("Anton");

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async void FindAuthorByNameTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var repository = new CheepRepository(context);
        var authorGuid = Guid.NewGuid();
        context.Cheeps.Add(new Cheep() { CheepId = Guid.NewGuid(), AuthorId = authorGuid, Author = new Author() { AuthorId = authorGuid, Name = "Anton", Email = "anlf@itu.dk" }, Text = "Hej, velkommen til kurset.", TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
        context.SaveChanges();

        // Act
        var result = await repository.FindAuthorByName("Anton");
        if (result == null) 
        {
            Assert.True(false);
        }
        var authorCheepsList = result.Cheeps;

        // Assert
        Assert.Equal("Anton", result.Name);
        Assert.Equal("anlf@itu.dk", result.Email);
        Assert.Single(authorCheepsList);
    }

    [Fact]
    public async void FindAuthorByEmailTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var repository = new CheepRepository(context);
        var authorGuid = Guid.NewGuid();
        context.Cheeps.Add(new Cheep() { CheepId = Guid.NewGuid(), AuthorId = authorGuid, Author = new Author() { AuthorId = authorGuid, Name = "Anton", Email = "anlf@itu.dk" }, Text = "Hej, velkommen til kurset.", TimeStamp = DateTime.Parse("2023-08-01 13:08:28") });
        context.SaveChanges();

        // Act
        var result = await repository.FindAuthorByEmail("anlf@itu.dk");
        if (result == null) 
        {
            Assert.True(false);
        }
        var authorCheepsList = result.Cheeps;

        // Assert
        Assert.Equal("Anton", result.Name);
        Assert.Equal("anlf@itu.dk", result.Email);
        Assert.Single(authorCheepsList);
    }

    [Fact]
    public async void CreateAuthorTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var repository = new CheepRepository(context);
        var authorGuid = Guid.NewGuid();
        context.SaveChanges();

        // Act
        repository.CreateAuthor("Anton", "anlf@itu.dk");
        Author? result = await repository.FindAuthorByEmail("anlf@itu.dk");
        if (result == null) 
        {
            Assert.True(false);
        }
        List<Cheep> authorCheepsList = result.Cheeps;

        // Assert
        Assert.Equal("Anton", result.Name);
        Assert.Equal("anlf@itu.dk", result.Email);
        Assert.Empty(authorCheepsList);
    }

     [Fact]
    public async void CreateCheepTest()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChirpContext>().UseSqlite(connection);
        using var context = new ChirpContext(builder.Options);
        await context.Database.EnsureCreatedAsync();
        var repository = new CheepRepository(context);
        var authorGuid = Guid.NewGuid();
        context.SaveChanges();

        // Act
        repository.CreateAuthor("Anton", "anlf@itu.dk");
        Author? author = await repository.FindAuthorByName("Anton");
        if (author == null) 
        {
            Assert.True(false);
        }
        repository.CreateCheep(author, "Clara er sej", DateTime.Parse("2023-08-01 13:08:28"));
        var result = await repository.GetCheeps(1, 1);

        // Assert
        Assert.Equal("Anton", result[0].author);
        Assert.Equal("Clara er sej", result[0].message);
        Assert.Equal("2023-08-01 13:08:28", result[0].timestamp);
        Assert.Single(result);
    }
}