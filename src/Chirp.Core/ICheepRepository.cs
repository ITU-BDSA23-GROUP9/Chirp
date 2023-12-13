namespace Chirp.Core;

public interface ICheepRepository
{
    public Task<List<CheepDTO>> GetCheeps(int limit, int pageNumber);
    public Task<List<CheepDTO>> GetCheepsFromAuthor(string author, int limit, int pageNumber);
    public Task<List<CheepDTO>> GetAllCheeps();
    public Task<int> GetTotalCheepCount();
    public Task<List<CheepDTO>> GetPrivateTimelineCheeps(string authorUsername, int limit, int pageNumber);
    public Task CreateCheep(CheepDTO cheep);
    public Task Like(string cheepId, string authorUsername);
    public Task Dislike(string cheepId, string authorUsername);
}