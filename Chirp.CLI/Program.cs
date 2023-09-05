using System.Globalization;
using Microsoft.VisualBasic.FileIO;

var path = "chirp_cli_db.csv";

if(args[0] == "read")
{
    using TextFieldParser parser = new TextFieldParser(path);
    parser.TextFieldType = FieldType.Delimited;
    parser.SetDelimiters(",");

    parser.ReadFields();

    while(!parser.EndOfData)
    {
        var currentRow = parser.ReadFields();
        var author = currentRow[0];
        var message = currentRow[1];
        var timestamp = currentRow[2];
        var dateTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(timestamp)).LocalDateTime;
        var formattedDate = dateTime.ToString("MM/dd/yy HH:mm:ss", CultureInfo.InvariantCulture);
        var formattedCheep = $"{author} @ {formattedDate}: {message}";

        Console.WriteLine(formattedCheep);
    }
}
else if(args[0] == "cheep")
{
    using StreamWriter writer = new StreamWriter(path, true);
    var userName = Environment.UserName;
    var currentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
    var message = args[1];
    var saveFormattedCheep = $"{userName},\"{message}\",{currentTimestamp}";
    writer.WriteLine(saveFormattedCheep);
}