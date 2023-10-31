using Microsoft.EntityFrameworkCore;
public class AuthorRepository : IAuthorRepository 
{
    private readonly ChirpContext _db;
    public AuthorRepository(ChirpContext db)
    {
        _db = db;
    }
    public async Task<int> GetTotalCheepCountFromAuthor(string author)
    {
        return await _db.Cheeps
        .Where(cheep => cheep.Author.Name == author)
        .CountAsync();
    }

    public async Task<AuthorDTO?> FindAuthorByName(string author)
    {
        Author? authorModel = await _db.Authors.FirstOrDefaultAsync(a => a.Name == author);
        if (authorModel == null)
        {
            throw new Exception("Author does not exist");
        }
        return new AuthorDTO(authorModel.Name, authorModel.Email);
    }

    public async Task<AuthorDTO?> FindAuthorByEmail(string email)
    {
        Author? authorModel = await _db.Authors.FirstOrDefaultAsync(a => a.Email == email);
        if (authorModel == null)
        {
            throw new Exception("Author does not exist");
        }
        return new AuthorDTO(authorModel.Name, authorModel.Email);
    }

    public void CreateAuthor(string name, string email)
    {
        var author = new Author()
        {
            AuthorId = Guid.NewGuid(),
            Name = name,
            Email = email
        };
        _db.Authors.AddRange(author);
        _db.SaveChanges();
    }
}