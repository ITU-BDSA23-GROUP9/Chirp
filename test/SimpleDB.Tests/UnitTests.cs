using System.Net.Security;
using SimpleDB;
namespace SimpleDB.Tests;

public class UnitTests
{
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
        for (int i = 0; i < 4; i++)
        {
            Assert.Equal(constantcheeps[i].Author, databaseCheeps[i].Author);
            Assert.Equal(constantcheeps[i].Message, databaseCheeps[i].Message);
            Assert.Equal(constantcheeps[i].Timestamp, databaseCheeps[i].Timestamp);
        };

    }
}
