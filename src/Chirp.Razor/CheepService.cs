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
    public List<CheepViewModel> GetCheeps()
    {
        var sqlDBFilePath = Environment.GetEnvironmentVariable("CHIRPDBPATH") ?? Path.Combine(Path.GetTempPath(), "chirp.db");
        var schemaSQL = File.ReadAllText("../../data/schema.sql");
        var dataDumpSQL = File.ReadAllText("../../data/dump.sql");
        var query = @"SELECT U.username, M.text, M.pub_date FROM message M JOIN user U WHERE U.user_id = M.author_id";

        using (var connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
        {
            connection.Open();

            SqliteCommand loadSchema = connection.CreateCommand();
            loadSchema.CommandText = schemaSQL;
            loadSchema.ExecuteNonQuery();

            SqliteCommand loadDataDump = connection.CreateCommand();
            loadDataDump.CommandText = dataDumpSQL;
            loadDataDump.ExecuteNonQuery();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = query;
            using SqliteDataReader reader = command.ExecuteReader();
            List<CheepViewModel> cheeps = new();

            while (reader.Read())
            {
                cheeps.Add(new CheepViewModel(reader.GetString(0), reader.GetString(1), UnixTimeStampToDateTimeString(reader.GetDouble(2))));
            }

            return cheeps;
        }
    }

    public List<CheepViewModel> GetCheepsFromAuthor(string author)
    {
        var sqlDBFilePath = "../../data/chirp.db";
        var query = @"SELECT U.username, M.text, M.pub_date FROM message M JOIN user U ON U.user_id = M.author_id WHERE U.username = $author";
        
        using (var connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("$author", author);

            using var reader = command.ExecuteReader();

            List<CheepViewModel> cheeps = new();

            while (reader.Read())
            {
                cheeps.Add(new CheepViewModel(reader.GetString(0), reader.GetString(1), UnixTimeStampToDateTimeString(reader.GetDouble(2))));
            }

            return cheeps;
        }
    }

    private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        CultureInfo cultureInfo = new("en-US"); // Use the "en-US" culture

        // Unix timestamp is seconds past epoch
        DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss", cultureInfo);
    }

}
