using Chirp.Core;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;
public class CheepRepository : ICheepRepository
{
    private readonly ChirpContext _db;
    private readonly CheepCreateValidator _validator;
    public CheepRepository(ChirpContext db, CheepCreateValidator validator)
    {
        _db = db;
        _validator = validator;
    }

    public async Task<List<CheepDTO>> GetCheeps(int limit, int pageNumber)
    {
        List<CheepDTO> cheeps = await _db.Cheeps
        .OrderByDescending(cheep => cheep.TimeStamp)
        .Skip(limit * (pageNumber - 1))
        .Take(limit)
        .Select(cheep => new CheepDTO(cheep.Text, cheep.Author.UserName!, cheep.TimeStamp.ToString()))
        .ToListAsync();

        return cheeps;
    }

    public async Task<List<CheepDTO>> GetAllCheeps()
    {
        List<CheepDTO> cheeps = await _db.Cheeps
        .OrderByDescending(cheep => cheep.TimeStamp)
        .Select(cheep => new CheepDTO(cheep.Text, cheep.Author.UserName!, cheep.TimeStamp.ToString()))
        .ToListAsync();

        return cheeps;
    }

    public async Task<List<CheepDTO>> GetCheepsFromAuthor(string author, int limit, int pageNumber)
    {
        int cheepsToSkip = (pageNumber - 1) * limit;

        var authorModel = await FindAuthorModelByName(author);

        List<CheepDTO> cheeps = await _db.Cheeps
            .Where(cheep => cheep.Author.Id == authorModel.Id)
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip(cheepsToSkip)
            .Take(limit)
            .Select(cheep => new CheepDTO(cheep.Text, cheep.Author.UserName!, cheep.TimeStamp.ToString()))
            .ToListAsync();

        return cheeps;
    }

    public async Task<int> GetTotalCheepCount()
    {
        return await _db.Cheeps.CountAsync();
    }

    private async Task<Author> FindAuthorModelByName(string author)
    {
        Author? authorModel = await _db.Authors
        .Include(a => a.Cheeps)
        .Include(a => a.Following)
        .AsSplitQuery()
        .FirstOrDefaultAsync(a => a.UserName == author);
        if (authorModel == null)
        {
            throw new Exception("Author does not exist");
        }
        return authorModel;
    }

    public async Task CreateCheep(CheepDTO cheep)
    {
        var validationResult = await _validator.ValidateAsync(cheep);

        if (!validationResult.IsValid)
        {
            throw new Exception("The cheep can be no more than 160 characters long!");
        }
        var author = await FindAuthorModelByName(cheep.author);
        var newCheep = new Cheep { CheepId = Guid.NewGuid().ToString(), Author = author, Text = cheep.message, TimeStamp = DateTime.Parse(cheep.timestamp) };
        author.Cheeps.Add(newCheep);
        _db.Cheeps.AddRange(newCheep);
        _db.SaveChanges();

    }

    public async Task<List<CheepDTO>> GetPrivateTimelineCheeps(string authorUsername, int limit, int pageNumber)
    {
        int cheepsToSkip = (pageNumber - 1) * limit;
        var author = await FindAuthorModelByName(authorUsername);
        var following = author.Following;
        var cheeps = author.Cheeps;
        foreach (Author user in following)
        {
            var followerAuthor = await FindAuthorModelByName(user.UserName!);
            cheeps.AddRange(followerAuthor.Cheeps);
        }

        return cheeps.OrderByDescending(cheep => cheep.TimeStamp)
            .Skip(cheepsToSkip)
            .Take(limit)
            .Select(cheep => new CheepDTO(cheep.Text, cheep.Author.UserName!, cheep.TimeStamp.ToString()))
            .ToList();
    }
}