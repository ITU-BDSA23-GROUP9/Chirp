using Microsoft.EntityFrameworkCore;

public class ChirpContext : DbContext
{
    public DbSet<Cheep>? Cheeps { get; set; }
    public DbSet<Author>? Authors { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlite($"Data Source={"./mychirp.db"}");
}

public class Cheep
{
    public required int CheepId { get; set; }
    public required string Message { get; set; }
    public required Author Author { get; set; }
    public DateTime Timestamp { get; set; }


}

public class Author
{
    public required int AuthorId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public List<Cheep> Cheeps { get; } = new();
}

