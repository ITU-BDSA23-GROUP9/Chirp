using System.Globalization;
using Microsoft.VisualBasic.FileIO;

var path = "chirp_cli_db.csv";

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