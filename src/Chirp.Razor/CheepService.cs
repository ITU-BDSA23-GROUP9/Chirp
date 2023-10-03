using System.Globalization;
using Microsoft.Data.Sqlite;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepViewModel> GetCheeps();
    public List<CheepViewModel> GetCheepsFromAuthor(string author);
}

public class CheepService : ICheepService
{
    // These would normally be loaded from a database for example
    private static readonly List<CheepViewModel> _cheeps = new()
        {
            new CheepViewModel("Helge", "Hello, BDSA students!", UnixTimeStampToDateTimeString(1690892208)),
            new CheepViewModel("Rasmus", "Hej, velkommen til kurset.", UnixTimeStampToDateTimeString(1690895308)),
        };

    public List<CheepViewModel> GetCheeps()
    {
        var sqlDBFilePath = "/tmp/chirp.db";
        var query = @"SELECT U.username, M.text, M.pub_date FROM message M JOIN user U WHERE U.user_id = M.author_id";
        using (var connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = query;
            using var reader = command.ExecuteReader();
            
            List<CheepViewModel> cheeps = new ();

            while(reader.Read())
            {
                cheeps.Add(new CheepViewModel(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
            }

            return cheeps;
        }
    }

    public List<CheepViewModel> GetCheepsFromAuthor(string author)
    {
        // filter by the provided author name
        return _cheeps.Where(x => x.Author == author).ToList();
    }

    private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        CultureInfo cultureInfo = new CultureInfo("en-US"); // Use the "en-US" culture

        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss", cultureInfo);
    }

}
