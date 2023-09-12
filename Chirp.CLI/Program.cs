using System.Globalization;
using CsvHelper;
using SimpleDB;
using UI;
using DocoptNet;

IDatabaseRepository<Cheep> database = new CSVDatabase<Cheep>();

Cheep CreateCheep(string message) {
    var userName = Environment.UserName;
    var currentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
    return new Cheep(userName, message, currentTimestamp);
}

const string usage = @"
Usage:
    dotnet run read
    dotnet run cheep <message>
";
var arguments = new Docopt().Apply(usage, args, exit: true);

if (arguments["read"].IsTrue)
{
    UserInterface.PrintCheeps(database.Read());
}
else if(args[0] == "cheep")
{
    database.Store(CreateCheep(args[1]));
}