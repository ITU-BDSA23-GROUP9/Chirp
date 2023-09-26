using SimpleDB;
using UI;
using DocoptNet;
using Utilities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

IDatabaseRepository<Cheep> database = CSVDatabase<Cheep>.getInstance("../../data/chirp_cli_db.csv");

static Cheep CreateCheep(string message)
{
    var userName = Environment.UserName;
    var currentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
    return new Cheep(userName, message, currentTimestamp);
}

const string usage = @"Chirp.

Usage:
    Chirp.CLI.dll read
    Chirp.CLI.dll cheep <message>
";

var arguments = new Docopt().Apply(usage, args, exit: true)!;

if (arguments["read"].IsTrue)
{
    var cheeps = await WebServiceClient.Get();
    UserInterface.PrintCheeps(cheeps);
}
else if (arguments["cheep"].IsTrue)
{
    var message = arguments["<message>"].ToString();
    await WebServiceClient.Post(CreateCheep(message));
}