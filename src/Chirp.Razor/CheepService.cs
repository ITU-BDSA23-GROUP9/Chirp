using System.Globalization;
using System.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.FileProviders;
using Microsoft.VisualBasic;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepViewModel> GetCheeps();
    public List<CheepViewModel> GetCheepsFromAuthor(string author);
}

public class CheepService : ICheepService
{

    DBFacade facade = DBFacade.getInstance();
    public CheepService() {
    }

    public List<CheepViewModel> GetCheeps() 
    {
        return facade.GetCheeps();
    }
    public List<CheepViewModel> GetCheepsFromAuthor(string author)
    {
        return facade.GetCheepsFromAuthor(author);
    }
}
