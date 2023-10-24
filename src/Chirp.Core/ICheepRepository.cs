public interface ICheepRepository
{
    public Task<List<CheepDTO>> GetCheeps(int limit, int pageNumber);
    public Task<List<CheepDTO>> GetCheepsFromAuthor(string author, int limit, int pageNumber);
    public Task<List<CheepDTO>> GetAllCheeps();
    public Task<int> GetTotalCheepCount();
    public Task<int> GetTotalCheepCountFromAuthor(string author);
    public Task<AuthorDTO?> FindAuthorByName(string author);
    public Task<AuthorDTO?> FindAuthorByEmail(string email);
    public void CreateAuthor(string name, string email);
    public void CreateCheep(AuthorDTO author, string text, DateTime timestamp);
}