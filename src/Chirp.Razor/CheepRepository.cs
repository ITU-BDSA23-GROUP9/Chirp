using System.Net.Sockets;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
public class CheepRepository : ICheepRepository
{
    private readonly ChirpContext _db;
    public CheepRepository(ChirpContext db)
    {
        _db = db;
    }

    public async Task<List<CheepDTO>> GetCheeps(int limit, int pageNumber)
    {
        List<CheepDTO> cheeps = await _db.Cheeps
        .OrderByDescending(cheep => cheep.TimeStamp)
        .Skip(limit * (pageNumber - 1))
        .Take(limit)
        .Select(cheep => new CheepDTO(cheep.Text, cheep.Author.Name, cheep.TimeStamp.ToString()))
        .ToListAsync();

        return cheeps;
    }

    public async Task<List<CheepDTO>> GetAllCheeps()
    {
        List<CheepDTO> cheeps = await _db.Cheeps
        .OrderByDescending(cheep => cheep.TimeStamp)
        .Select(cheep => new CheepDTO(cheep.Text, cheep.Author.Name, cheep.TimeStamp.ToString()))
        .ToListAsync();

        return cheeps;
    }

    public async Task<List<CheepDTO>> GetCheepsFromAuthor(string author, int limit, int pageNumber)
    {
        int cheepsToSkip = (pageNumber - 1) * limit;

        List<CheepDTO> cheeps = await _db.Cheeps
            .Where(cheep => cheep.Author.Name == author)
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip(cheepsToSkip)
            .Take(limit)
            .Select(cheep => new CheepDTO(cheep.Text, cheep.Author.Name, cheep.TimeStamp.ToString()))
            .ToListAsync();

        return cheeps;
    }

    public async Task<int> GetTotalCheepCount()
    {
        return await _db.Cheeps.CountAsync();
    }

    public async Task<int> GetTotalCheepCountFromAuthor(string author)
    {
        return await _db.Cheeps
        .Where(cheep => cheep.Author.Name == author)
        .CountAsync();
    }

    public async Task<Author?> FindAuthorByName(string author)
    {
        return await _db.Authors.FirstOrDefaultAsync(a => a.Name == author);
    }

    public async Task<Author?> FindAuthorByEmail(string email)
    {
        return await _db.Authors.FirstOrDefaultAsync(a => a.Email == email);
    }

    public void CreateAuthor(string name, string email)
    {
        var author = new Author() { AuthorId = Guid.NewGuid(), Name = name, Email = email, Cheeps = new List<Cheep>() };
        _db.Authors.AddRange(author);
        _db.SaveChanges();
    }

    public void CreateCheep(Author author, string text, DateTime timestamp)
    {
        var cheep = new Cheep() { CheepId = Guid.NewGuid(), AuthorId = author.AuthorId, Author = author, Text = text, TimeStamp = timestamp };
        _db.Cheeps.AddRange(cheep);
        _db.SaveChanges();
    }

}