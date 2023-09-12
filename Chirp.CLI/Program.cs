using System.Globalization;
using CsvHelper;
using SimpleDB;
string formatDateTime(long timestamp)
{
    var dateTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(timestamp)).LocalDateTime;
    return dateTime.ToString("MM/dd/yy HH:mm:ss", CultureInfo.InvariantCulture);
}

Cheep createCheep(string message) {
    var userName = Environment.UserName;
    var currentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
    return new Cheep(userName, message, currentTimestamp);
}
void readCheep(Cheep cheep)
{
    var formattedDate = formatDateTime(cheep.Timestamp);
    var formattedCheep = $"{cheep.Author} @ {formattedDate}: {cheep.Message}";

    Console.WriteLine(formattedCheep);
}


IDatabaseRepository<Cheep> database = new CSVDatabase<Cheep>();
if(args[0] == "read")
{
    foreach (var cheep in database.Read())
    {
        readCheep(cheep);
    }
}
else if(args[0] == "cheep")
{
    database.Store(createCheep(args[1]));
}