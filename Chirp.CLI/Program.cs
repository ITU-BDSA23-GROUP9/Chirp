using System.Globalization;
using CsvHelper;

IEnumerable<Cheep> readCheeps() 
{
    using StreamReader reader = new("chirp_cli_db.csv");
    using CsvReader csvReader = new(reader, CultureInfo.InvariantCulture);

    var cheeps = csvReader.GetRecords<Cheep>().ToList();
    return cheeps;
}

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

if(args[0] == "read")
{
    foreach (var cheep in readCheeps())
    {
        readCheep(cheep);
    }
}
else if(args[0] == "cheep")
{
    writeCheep(args[1]);
}