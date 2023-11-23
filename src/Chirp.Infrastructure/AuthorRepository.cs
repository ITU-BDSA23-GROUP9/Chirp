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
        .Where(cheep => cheep.Author.UserName == author)
        .CountAsync();
    }

    public async Task<AuthorDTO?> FindAuthorByName(string author)
    {
        Author? authorModel = await _db.Authors.FirstOrDefaultAsync(a => a.UserName == author);
        if (authorModel == null)
        {
            throw new Exception("Author does not exist");
        }
        return new AuthorDTO(authorModel.UserName, authorModel.Email);
    }

    public async Task<AuthorDTO?> FindAuthorByEmail(string email)
    {
        Author? authorModel = await _db.Authors.FirstOrDefaultAsync(a => a.Email == email);
        if (authorModel == null)
        {
            throw new Exception("Author does not exist");
        }
        return new AuthorDTO(authorModel.UserName, authorModel.Email);
    }

    public void CreateAuthor(string name, string email)
    {
        var author = new Author()
        {
            Id = Guid.NewGuid().ToString().ToString(),
            UserName = name,
            Email = email
        };
        _db.Authors.AddRange(author);
        _db.SaveChanges();
    }

    public async Task Follow(string authorWhoWantsToFollow, string authorToFollow)
    {
        Author authorWhoWantsToFollowModel = await FindAuthorModelByName(authorWhoWantsToFollow);
        Author authorToFollowModel = await FindAuthorModelByName(authorToFollow);
        authorWhoWantsToFollowModel.Following.Add(authorToFollowModel);
        AddFollower(authorWhoWantsToFollowModel, authorToFollowModel);
        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                if (entry.Entity is Author)
                {
                    var proposedValues = entry.CurrentValues;
                    var databaseValues = entry.GetDatabaseValues();

                    foreach (var property in proposedValues.Properties)
                    {
                        var proposedValue = proposedValues[property];
                        var databaseValue = databaseValues[property];
                    }
                }
            }
        }
    }

    private void AddFollower(Author authorWhoWantsToFollow, Author authorToFollow)
    {
        authorToFollow.Followers.Add(authorWhoWantsToFollow);
    }

    public async Task<Author> FindAuthorModelByName(string author)
    {
        Author? authorModel = await _db.Authors.FirstOrDefaultAsync(a => a.UserName == author);
        if (authorModel == null)
        {
            throw new Exception("Author does not exist: " + author);
        }
        return authorModel;
    }

    public async Task<bool> IsUserFollowingAuthor(string authorUsername, string username)
    {
        var user = await FindAuthorModelByName(username);
        var author = await FindAuthorModelByName(authorUsername);
        return user.Following.Contains(author);
    }
}