using Microsoft.AspNetCore.Identity;

public class Author : IdentityUser
{
    public List<Cheep> Cheeps { get; set; } = new();
    public ICollection<Author>? Followers { get; set; } 
    public ICollection<Author>? Following { get; set; }
    
}