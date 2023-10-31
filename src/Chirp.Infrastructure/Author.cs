using Microsoft.AspNetCore.Identity;

public class Author : IdentityUser
{
    public required Guid AuthorId { get; set; }
    public required string Name { get; set; }
    public required string Mail { get; set; }
    public List<Cheep> Cheeps { get; set; } = new();
}