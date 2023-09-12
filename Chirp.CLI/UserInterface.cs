namespace UI;
using System.Globalization;
public static class UserInterface 
{
    static string FormatDateTime(long timestamp)
    {
        var dateTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(timestamp)).LocalDateTime;
        return dateTime.ToString("MM/dd/yy HH:mm:ss", CultureInfo.InvariantCulture);
    }
    static void ReadCheep(Cheep cheep)
    {
        var formattedDate = FormatDateTime(cheep.Timestamp);
        var formattedCheep = $"{cheep.Author} @ {formattedDate}: {cheep.Message}";

        Console.WriteLine(formattedCheep);
    }
    public static void PrintCheeps (IEnumerable<Cheep> cheeps) 
    {
        foreach (var cheep in cheeps)
        {
            ReadCheep(cheep);
        }
    }
}