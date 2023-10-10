using Microsoft.EntityFrameworkCore;

public class ChirpContext : DbContext
{
    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }

    public ChirpContext(DbContextOptions<ChirpContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cheep>().ToTable("message");
        modelBuilder.Entity<Author>().ToTable("user");
    }
}

public class Cheep
{
    public required int CheepId { get; set; }
    public required int AuthorId { get; set; }
    public required Author Author { get; set; }
    public required string Text { get; set; }
    public DateTime TimeStamp { get; set; }


}

public class Author
{
    public required int AuthorId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public List<Cheep> Cheeps { get; set; } = new();
}

public class CheepDTO
{
    public string Message { get; set; }
    public string Author { get; set; }
    public string Timestamp { get; set; }

    public CheepDTO(string message, string author, string timestamp)
    {
        Message = message;
        Author = author;
        Timestamp = timestamp;
    }
}
