using UI;

namespace Chirp.CLI.Tests;

public class UnitTests
{
    [Fact]
    public void TestCheepRecord()
    {
        // Arrange
        var cheep = new Cheep("lana", "hello world", 1695117316);
        // Act
        var name = cheep.Author;
        var message = cheep.Message;
        var timestamp = cheep.Timestamp;
        // Assert
        Assert.Equal("lana", name);
        Assert.Equal("hello world", message);
        Assert.Equal(1695117316, timestamp);
    }

    [Fact]
    public void FormatDatetimeTest()
    {
        // Arrange
        var unixTimestamp = 1695117316;
        string stringDatetimeTimestamp;

        //Act
        stringDatetimeTimestamp = UserInterface.FormatDateTime(unixTimestamp);

        //Assert
        Assert.Equal("09/19/23 11:55:16", stringDatetimeTimestamp);

    }
}
