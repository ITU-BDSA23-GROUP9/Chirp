using Microsoft.AspNetCore.Identity;

namespace Chirp.Infrastructure;
public class Author : IdentityUser
{
    public List<Cheep> Cheeps { get; set; } = new();
    public List<Author> Followers { get; set; } = new();
    public List<Author> Following { get; set; } = new();

}