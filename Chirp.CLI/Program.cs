using System.Globalization;
using CsvHelper;
using SimpleDB;
using UI;


Cheep createCheep(string message) {
    var userName = Environment.UserName;
    var currentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
    return new Cheep(userName, message, currentTimestamp);
}


IDatabaseRepository<Cheep> database = new CSVDatabase<Cheep>();
if(args[0] == "read")
{
    UserInterface.PrintCheeps(database.Read());
}
else if(args[0] == "cheep")
{
    database.Store(createCheep(args[1]));
}