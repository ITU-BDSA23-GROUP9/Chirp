
public interface IAuthorRepository 
{
    public Task<int> GetTotalCheepCountFromAuthor(string author);
    public Task<AuthorDTO?> FindAuthorByName(string author);
    public Task<AuthorDTO?> FindAuthorByEmail(string email);
    public void CreateAuthor(string name, string email);
}