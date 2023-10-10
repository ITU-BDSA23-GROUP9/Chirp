using System.Globalization;
using System.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Linq;
public class CheepRepository : ICheepRepository
{
    ChirpContext _db;
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

    public async Task<List<CheepViewModel>> GetCheeps(int limit, int pageNumber)
    {
        List<Cheep> cheeps = await _db.Cheeps
        .Skip(limit * pageNumber)
        .Take(limit)
        .ToListAsync();

        List<CheepViewModel> CheepVMList = new();

        cheeps.ForEach(cheep => CheepVMList.Add(new CheepViewModel(cheep.Author.Name, cheep.Message, cheep.Timestamp.ToString())));

        return CheepVMList;
    }

    public async Task<List<CheepViewModel>> GetCheepsFromAuthor(string author, int limit, int pageNumber)
    {
        List<Cheep> cheeps = await _db.Cheeps
        .Skip(limit * pageNumber)
        .Take(limit)
        .Where(cheep => cheep.Author.Name == author)
        .ToListAsync();

        List<CheepViewModel> CheepVMList = new();

        cheeps.ForEach(cheep => CheepVMList.Add(new CheepViewModel(cheep.Author.Name, cheep.Message, cheep.Timestamp.ToString())));

        return CheepVMList;
    }

}