using System.Net.Security;
using SimpleDB;
namespace SimpleDB.Tests;

public class UnitTests
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
    public void DatabaseReadTest()
    {
        // Arrange
        List<Cheep> constantcheeps = new List<Cheep>{
        new Cheep("ropf", "Hello, BDSA students!", 1690891760),
        new Cheep("rnie", "Welcome to the course!", 1690978778),
        new Cheep("rnie", "I hope you had a good summer.", 1690979858),
        new Cheep("ropf", "Cheeping cheeps on Chirp :)", 1690981487)
        };

        // Act
        var databaseCheeps = SetupTestDatabase().Read().ToArray();

        // Assert
        for (int i = 0; i < databaseCheeps.Length - 1; i++)
        {
            Assert.Equal(constantcheeps[i].Author, databaseCheeps[i].Author);
            Assert.Equal(constantcheeps[i].Message, databaseCheeps[i].Message);
            Assert.Equal(constantcheeps[i].Timestamp, databaseCheeps[i].Timestamp);
        };

        RemoveTestDatabase();
    }

    [Fact]
    public void DatabaseWriteTest()
    {
        // Arrange
        List<Cheep> constantcheeps = new List<Cheep>{
        new Cheep("ropf", "Hello, BDSA students!", 1690891760),
        new Cheep("rnie", "Welcome to the course!", 1690978778),
        new Cheep("rnie", "I hope you had a good summer.", 1690979858),
        new Cheep("ropf", "Cheeping cheeps on Chirp :)", 1690981487),
        new Cheep("kram", "hello everyone", 1690981488)
        };

        // Act
        var database = SetupTestDatabase();
        var testcheep = new Cheep("kram", "hello everyone", 1690981488);
        database.Store(testcheep);
        var databaseCheeps = database.Read().ToArray();
        RemoveTestDatabase();

        // Assert
        for (int i = 0; i < databaseCheeps.Length - 1; i++)
        {
            Assert.Equal(constantcheeps[i].Author, databaseCheeps[i].Author);
            Assert.Equal(constantcheeps[i].Message, databaseCheeps[i].Message);
            Assert.Equal(constantcheeps[i].Timestamp, databaseCheeps[i].Timestamp);
        };
    }
}
