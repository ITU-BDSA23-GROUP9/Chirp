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
    using StreamWriter writer = new(path, true);
    using CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture);

    var userName = Environment.UserName;
    var currentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
    var saveCheep = new Cheep(userName, message, currentTimestamp);
    csvWriter.WriteRecord(saveCheep);
    writer.WriteLine();
}

void Main(string[] args)
{
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
}