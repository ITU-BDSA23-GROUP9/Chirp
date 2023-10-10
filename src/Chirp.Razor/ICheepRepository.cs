public interface ICheepRepository
{
    public Task<List<Cheep>> GetCheeps(int limit, int pageNumber);
    public Task<List<Cheep>> GetCheeps();
    public Task<List<Cheep>> GetCheepsFromAuthor(string author, int limit, int pageNumber);
}