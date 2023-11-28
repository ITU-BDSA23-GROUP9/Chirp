namespace Chirp.Core;

public interface ICheepRepository
{
    public Task<List<CheepDTO>> GetCheeps(int limit, int pageNumber);
    public Task<List<CheepDTO>> GetCheepsFromAuthor(string author, int limit, int pageNumber);
    public Task<List<CheepDTO>> GetAllCheeps();
    public Task<int> GetTotalCheepCount();
    public Task CreateCheep(AuthorDTO author, string text, DateTime timestamp);

    public Task AddCheep(CheepDTO cheep, DateTime timestamp);
}