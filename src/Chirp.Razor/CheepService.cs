public interface ICheepService
{
    public Task<List<CheepDTO>> GetCheeps(int limit, int pageNumber);
    public Task<List<CheepDTO>> GetCheeps();
    public Task<List<CheepDTO>> GetCheepsFromAuthor(string author, int limit, int pageNumber);
}

public class CheepService : ICheepService
{

    ICheepRepository _repository;
    public CheepService(ICheepRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CheepDTO>> GetCheeps()
    {
        var cheeps = await _repository.GetCheeps();

        List<CheepDTO> CheepDTOList = new();

        cheeps.ForEach(cheep => CheepDTOList.Add(new CheepDTO(cheep.Text, cheep.Author.Name, cheep.TimeStamp.ToString())));
        return CheepDTOList;
    }

    public async Task<List<CheepDTO>> GetCheeps(int limit, int pageNumber)
    {
        var cheeps = await _repository.GetCheeps(limit, pageNumber);

        List<CheepDTO> CheepDTOList = new();

        cheeps.ForEach(cheep => CheepDTOList.Add(new CheepDTO(cheep.Text, cheep.Author.Name, cheep.TimeStamp.ToString())));
        return CheepDTOList;
    }

    public async Task<List<CheepDTO>> GetCheepsFromAuthor(string author, int limit, int pageNumber)
    {
        var cheeps = await _repository.GetCheepsFromAuthor(author, limit, pageNumber);

        List<CheepDTO> CheepDTOList = new();

        cheeps.ForEach(cheep => CheepDTOList.Add(new CheepDTO(cheep.Text, cheep.Author.Name, cheep.TimeStamp.ToString())));
        return CheepDTOList;
    }
}
