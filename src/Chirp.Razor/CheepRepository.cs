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
        .Skip(limit * pageNumber)
        .Take(limit)
        .Select(cheep => new CheepDTO(cheep.Text, cheep.Author.Name, cheep.TimeStamp.ToString()))
        .ToListAsync();

        return cheeps;
    }

    public async Task<List<CheepDTO>> GetCheeps()
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
        .Skip(limit * pageNumber)
        .Take(limit)
        .Where(cheep => cheep.Author.Name == author)
        .Select(cheep => new CheepDTO(cheep.Text, cheep.Author.Name, cheep.TimeStamp.ToString()))
        .ToListAsync();

        return cheeps;
    }

}