using SimpleDB;
using UI;

namespace Chirp.CLI.Tests;

public class IntegrationTests
{
    private CSVDatabase<Cheep> CreateTestDatabase()
    {
        var fileName = "csvTestDB.csv";
        File.Create(fileName).Close();
        File.WriteAllText(fileName, "Author,Message,Timestamp");
        return new CSVDatabase<Cheep>(fileName);
    }

    private CSVDatabase<Cheep> PopulateTestDatabase(CSVDatabase<Cheep> db)
    {
        List<Cheep> constantcheeps = new List<Cheep>{
        new Cheep("ropf", "Hello, BDSA students!", 1690891760),
        new Cheep("rnie", "Welcome to the course!", 1690978778),
        new Cheep("rnie", "I hope you had a good summer.", 1690979858),
        new Cheep("ropf", "Cheeping cheeps on Chirp :)", 1690981487)
        };

        for (int i = 0; i < constantcheeps.ToArray().Length; i++)
        {
            db.Store(constantcheeps[i]);

        }

        return db;
    }

    private CSVDatabase<Cheep> SetupTestDatabase()
    {
        var db = CreateTestDatabase();
        PopulateTestDatabase(db);
        return db;
    }

    private void RemoveTestDatabase()
    {
        File.Delete("csvTestDB.csv");
    }

    [Fact]
    public void UIPrintCheepsIntegrationTest()
    {
        // Arrange
        var databasecheeps = SetupTestDatabase().Read();

        var formattedCheeps =
        "ropf @ 08/01/23 14:09:20: Hello, BDSA students!\nrnie @ 08/02/23 14:19:38: Welcome to the course!\nrnie @ 08/02/23 14:37:38: I hope you had a good summer.\nropf @ 08/02/23 15:04:47: Cheeping cheeps on Chirp :)";
        var output = new StringWriter();
        Console.SetOut(output);

        //Act
        UserInterface.PrintCheeps(databasecheeps);

        //Assert
        Assert.Equal(formattedCheeps, output.ToString().Trim());

        RemoveTestDatabase();
    }
}
