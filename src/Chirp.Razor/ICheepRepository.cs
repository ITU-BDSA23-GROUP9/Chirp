public interface ICheepRepository
{
    public List<CheepViewModel> GetCheeps(int? limit = null);
    public List<CheepViewModel> GetCheepsFromAuthor(string author);
}