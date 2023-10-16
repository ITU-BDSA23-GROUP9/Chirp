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

    public string ReadEmbeddedResoruceAsString(string path)
    {
        // Method of reading embedded resource inspired by lecture slides: https://github.com/itu-bdsa/lecture_notes/blob/main/sessions/session_05/Slides.md
        var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly(), "Chirp.Razor.data");
        using var reader = embeddedProvider.GetFileInfo(path).CreateReadStream();
        using var sr = new StreamReader(reader);
        return sr.ReadToEnd();
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
        List<CheepDTO> cheeps = await _db.Cheeps
        .OrderByDescending(cheep => cheep.TimeStamp)
        .Skip(limit * (pageNumber - 1))
        .Take(limit)
        .Where(cheep => cheep.Author.Name == author)
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

    public void CreateCheep(int id, Author author, string text, DateTime timestamp)
    {
        if (DoesAuthorExist(author) == null)
        {
            CreateAuthor(author.Name, author.Email);
        }
        //CheepId is temporary!!! We need to make this GUID, so it's unique no matter what, right?
        var cheep = new Cheep() { CheepId = 999999, AuthorId = author.AuthorId, Author = author, Text = text, TimeStamp = DateTime.Parse("2023-08-01 13:14:37") };
        _db.Cheeps.AddRange(cheep);
        _db.SaveChanges();
    }

    public Author? DoesAuthorExist(Author author)
    {
        return _db.Authors.Find((Author a) => a.AuthorId == author.AuthorId);
    }
}