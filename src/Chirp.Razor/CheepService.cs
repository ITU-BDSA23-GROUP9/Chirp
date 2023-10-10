public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public Task<List<CheepViewModel>> GetCheeps(int limit, int pageNumber);
    public List<CheepViewModel> GetCheepsFromAuthor(string author, int limit, int pageNumber);
}

public class CheepService : ICheepService
{

    ICheepRepository _repository;
    public CheepService(ICheepRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CheepViewModel>> GetCheeps(int limit, int pageNumber)
    {
        return await _repository.GetCheeps(limit, pageNumber);
    }
    public List<CheepViewModel> GetCheepsFromAuthor(string author, int limit, int pageNumber)
    {
        return _repository.GetCheepsFromAuthor(author, limit, pageNumber).Result;
    }
}
