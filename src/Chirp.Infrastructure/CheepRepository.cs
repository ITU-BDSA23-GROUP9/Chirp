using Microsoft.EntityFrameworkCore;

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
        .Select(cheep => new CheepDTO(cheep.Text, cheep.Author.UserName, cheep.TimeStamp.ToString()))
        .ToListAsync();

        return cheeps;
    }

    public async Task<List<CheepDTO>> GetAllCheeps()
    {
        List<CheepDTO> cheeps = await _db.Cheeps
        .OrderByDescending(cheep => cheep.TimeStamp)
        .Select(cheep => new CheepDTO(cheep.Text, cheep.Author.UserName, cheep.TimeStamp.ToString()))
        .ToListAsync();

        return cheeps;
    }

    public async Task<List<CheepDTO>> GetCheepsFromAuthor(string author, int limit, int pageNumber)
    {
        int cheepsToSkip = (pageNumber - 1) * limit;

        var authorModel = await FindAuthorModelByName(author);

        List<CheepDTO> cheeps = await _db.Cheeps
            .Where(cheep => cheep.Author.AuthorId == authorModel.AuthorId)
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip(cheepsToSkip)
            .Take(limit)
            .Select(cheep => new CheepDTO(cheep.Text, cheep.Author.UserName, cheep.TimeStamp.ToString()))
            .ToListAsync();

        return cheeps;
    }

    public async Task<int> GetTotalCheepCount()
    {
        return await _db.Cheeps.CountAsync();
    }

    private async Task<Author> FindAuthorModelByName(string author)
    {
        Author? authorModel = await _db.Authors.FirstOrDefaultAsync(a => a.UserName == author);
        if (authorModel == null)
        {
            throw new Exception("Author does not exist");
        }
        return authorModel;
    }

    public async void CreateCheep(AuthorDTO authorDTO, string text, DateTime timestamp)
    {
        var author = await FindAuthorModelByName(authorDTO.name);

        var cheep = new Cheep() { CheepId = Guid.NewGuid(), AuthorId = Guid.NewGuid(), Author = author, Text = text, TimeStamp = timestamp };
        _db.Cheeps.AddRange(cheep);
        _db.SaveChanges();
    }
}