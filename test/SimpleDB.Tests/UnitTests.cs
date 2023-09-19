using System.Net.Security;
using SimpleDB;
namespace SimpleDB.Tests;

public class UnitTests
{
    public void CleanupAfterTest()
    {
        // Clear the content of the CSV file
        File.WriteAllText("../../../../testdata/testdatabase.csv", "Author,Message,Timestamp");
        List<Cheep> constantcheeps = new List<Cheep>{
        new Cheep("ropf", "Hello, BDSA students!", 1690891760),
        new Cheep("rnie", "Welcome to the course!", 1690978778),
        new Cheep("rnie", "I hope you had a good summer.", 1690979858),
        new Cheep("ropf", "Cheeping cheeps on Chirp :)", 1690981487)
        };


        var database = SetupDatabase();

        for (int i = 0; i < constantcheeps.ToArray().Length; i++)
        {
            database.Store(constantcheeps[i]);

        }
    }

    private CSVDatabase<Cheep> SetupDatabase()
    {
        return new CSVDatabase<Cheep>("../../../../testdata/testdatabase.csv");
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
        var databaseCheeps = SetupDatabase().Read().ToArray();

        // Assert
        for (int i = 0; i < databaseCheeps.Length - 1; i++)
        {
            Assert.Equal(constantcheeps[i].Author, databaseCheeps[i].Author);
            Assert.Equal(constantcheeps[i].Message, databaseCheeps[i].Message);
            Assert.Equal(constantcheeps[i].Timestamp, databaseCheeps[i].Timestamp);
        };

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
        var database = SetupDatabase();
        var testcheep = new Cheep("kram", "hello everyone", 1690981488);
        database.Store(testcheep);
        var databaseCheeps = database.Read().ToArray();
        CleanupAfterTest();

        // Assert
        for (int i = 0; i < databaseCheeps.Length - 1; i++)
        {
            Assert.Equal(constantcheeps[i].Author, databaseCheeps[i].Author);
            Assert.Equal(constantcheeps[i].Message, databaseCheeps[i].Message);
            Assert.Equal(constantcheeps[i].Timestamp, databaseCheeps[i].Timestamp);
        };
    }
}
