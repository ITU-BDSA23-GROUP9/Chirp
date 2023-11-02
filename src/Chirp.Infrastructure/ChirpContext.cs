using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class ChirpContext : IdentityDbContext<Author>
{
    public DbSet<Cheep> Cheeps => Set<Cheep>();
    public DbSet<Author> Authors => Set<Author>();

    public ChirpContext(DbContextOptions<ChirpContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Call the base OnModelCreating method
        modelBuilder.Entity<Cheep>().Property(cheep => cheep.Text).HasMaxLength(160);

        // Configure the relationship between Cheep and Author using a shadow property
        modelBuilder.Entity<Cheep>()
            .HasOne(cheep => cheep.Author)
            .WithMany(author => author.Cheeps);
    }
}