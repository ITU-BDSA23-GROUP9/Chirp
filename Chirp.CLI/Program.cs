using SimpleDB;
using UI;
using DocoptNet;

IDatabaseRepository<Cheep> database = new CSVDatabase<Cheep>();

static Cheep CreateCheep(string message) {
    var userName = Environment.UserName;
    var currentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
    return new Cheep(userName, message, currentTimestamp);
}

const string usage = @"Chirp.

Usage:
    Chirp.CLI.dll read
    Chirp.CLI.dll cheep <message>
";

var arguments = new Docopt().Apply(usage, args)!;

if (arguments["read"].IsTrue)
{
    UserInterface.PrintCheeps(database.Read());
}
else if (arguments["cheep"].IsTrue)
{
    var message = arguments["<message>"].ToString();
    database.Store(CreateCheep(message));
}