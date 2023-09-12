using System.Globalization;
using CsvHelper;

var path = "chirp_cli_db.csv" ?? throw new ArgumentNullException();

IEnumerable<Cheep> readCheeps() 
{
    using StreamReader reader = new(path);
    using CsvReader csvReader = new(reader, CultureInfo.InvariantCulture);

    var cheeps = csvReader.GetRecords<Cheep>().ToList();
    return cheeps;
}

if(args[0] == "read")
{
    foreach (var cheep in readCheeps())
    {
        var dateTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(cheep.Timestamp)).LocalDateTime;
        var formattedDate = dateTime.ToString("MM/dd/yy HH:mm:ss", CultureInfo.InvariantCulture);
        var formattedCheep = $"{cheep.Author} @ {formattedDate}: {cheep.Message}";

        Console.WriteLine(formattedCheep);
    }
}
else if(args[0] == "cheep")
{
    using StreamWriter writer = new(path, true);
    using CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture);

    var userName = Environment.UserName;
    var currentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
    var message = args[1];
    var saveCheep = new Cheep(userName, message, currentTimestamp);
    csvWriter.WriteRecord(saveCheep);
    writer.WriteLine();
}