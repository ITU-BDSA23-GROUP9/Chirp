namespace Chirp.Core;
public interface IAuthorRepository
{
    public Task<int> GetTotalCheepCountFromAuthor(string author);
    public Task<AuthorDTO?> FindAuthorByName(string author);
    public Task<AuthorDTO?> FindAuthorByEmail(string email);
    public void CreateAuthor(string name, string email);
    public Task<bool> IsUserFollowingAuthor(string authorUsername, string username);
    public Task Follow(string authorWhoWantsToFollow, string authorToFollow);
    public Task Unfollow(string authorWhoWantsToFollow, string authorToFollow);
    public Task<int> GetTotalCheepCountFromFollowersAndAuthor(string authorUsername);
    public Task<List<AuthorDTO>> GetFollowers(string authorUsername);

}