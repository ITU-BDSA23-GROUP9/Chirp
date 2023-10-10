using System.Globalization;
using System.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Linq;
public class CheepRepository : ICheepRepository
{
    ChirpContext _db;
    public CheepRepository(ChirpContext db)
    {
        _db = db;
    }

    public string ReadEmbeddedResoruceAsString(string path)
    {
        // Method of reading embedded resource inspired by lecture slides: https://github.com/itu-bdsa/lecture_notes/blob/main/sessions/session_05/Slides.md
        var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly(), "Chirp.Razor.data");
        using var reader = embeddedProvider.GetFileInfo(path).CreateReadStream();
        using var sr = new StreamReader(reader);
        return sr.ReadToEnd();
    }

    public async Task<List<CheepViewModel>> GetCheeps(int? limit = null)
    {
        var cheeps = await _db.Cheeps.ToListAsync();
        return cheeps;
    }

    public List<CheepViewModel> GetCheepsFromAuthor(string author)
    {
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