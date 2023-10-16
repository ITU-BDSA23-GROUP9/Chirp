public interface ICheepRepository
{
    public Task<List<CheepDTO>> GetCheeps(int limit, int pageNumber);
    public Task<List<CheepDTO>> GetCheepsFromAuthor(string author, int limit, int pageNumber);
    public Task<List<CheepDTO>> GetAllCheeps();
    public Task<int> GetTotalCheepCount();
    public Task<int> GetTotalCheepCountFromAuthor(string author);
    public Task<Author?> FindAuthorByName(string author);
    public Task<Author?> FindAuthorByEmail(string email);
    public void CreateAuthor(string name, string email);
    public void CreateCheep(int id, Author author, string text, DateTime timestamp);
}