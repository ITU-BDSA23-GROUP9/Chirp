public interface ICheepRepository
{
    public Task<List<CheepViewModel>> GetCheeps(int limit, int pageNumber);
    public Task<List<CheepViewModel>> GetCheepsFromAuthor(string author, int limit, int pageNumber);
}