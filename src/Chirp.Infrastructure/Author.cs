using Microsoft.AspNetCore.Identity;

public class Author : IdentityUser
{
    public List<Cheep> Cheeps { get; set; } = new();
}