public class Author
{
    public required Guid AuthorId { get; set; }
    public required string Name { get; set; }

    public required string Email { get; set; }
    public List<Cheep> Cheeps { get; set; } = new();
}