using System.Globalization;
using CsvHelper;
using SimpleDB;
string formatDateTime(long timestamp)
{
    var dateTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(timestamp)).LocalDateTime;
    return dateTime.ToString("MM/dd/yy HH:mm:ss", CultureInfo.InvariantCulture);
}

void readCheep(Cheep cheep)
{
    var formattedDate = formatDateTime(cheep.Timestamp);
    var formattedCheep = $"{cheep.Author} @ {formattedDate}: {cheep.Message}";

    Console.WriteLine(formattedCheep);
}

void writeCheep(string message)
{
    using StreamWriter writer = new("chirp_cli_db.csv", true);
    using CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture);

    var userName = Environment.UserName;
    var currentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
    var saveCheep = new Cheep(userName, message, currentTimestamp);
    csvWriter.WriteRecord(saveCheep);
    writer.WriteLine();
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
    writeCheep(args[1]);
}