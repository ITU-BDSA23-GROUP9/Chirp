public interface ICheepRepository
{
    public Task<List<CheepDTO>> GetCheeps(int limit, int pageNumber);
    public Task<List<CheepDTO>> GetCheeps();
    public Task<List<CheepDTO>> GetCheepsFromAuthor(string author, int limit, int pageNumber);
}