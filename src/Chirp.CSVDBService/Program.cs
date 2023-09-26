using SimpleDB;
using Utilities;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/cheeps", () => CSVDatabase<Cheep>.getInstance("../../data/chirp_cli_db.csv").Read());
app.MapPost("/cheep", (Cheep cheep)=> CSVDatabase<Cheep>.getInstance("../../data/chirp_cli_db.csv").Store(cheep));


app.Run();
