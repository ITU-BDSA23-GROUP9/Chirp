using Microsoft.EntityFrameworkCore;

public class ChirpContext : DbContext
{
    public DbSet<Cheep> Cheeps => Set<Cheep>();
    public DbSet<Author> Authors => Set<Author>();

    public ChirpContext(DbContextOptions<ChirpContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cheep>().Property(cheep => cheep.Text).HasMaxLength(160);
    }
}

public class Cheep
{
    public required int CheepId { get; set; }
    public required Guid AuthorId { get; set; }
    public required Author Author { get; set; }
    public required string Text { get; set; }
    public DateTime TimeStamp { get; set; }
}

public class Author
{
    public required Guid AuthorId { get; set; }
    public required string Name { get; set; }

    public required string Email { get; set; }
    public List<Cheep> Cheeps { get; set; } = new();
}