using SimpleDB;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/cheeps", () => CSVDatabase.);
app.MapPost("/cheep", () => "Hello World!");


app.Run();
