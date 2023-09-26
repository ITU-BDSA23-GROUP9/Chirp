using UI;
using Utilities;

namespace Chirp.CLI.Tests;

public class UnitTests
{
    [Fact]
    public void CheepRecordTest()
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
    public void UIFormatDatetimeTest()
    {
        // Arrange
        var unixTimestamp = 1695117316;
        string stringDatetimeTimestamp;

        //Act
        stringDatetimeTimestamp = UserInterface.FormatDateTime(unixTimestamp);

        //Assert
        Assert.Equal("09/19/23 11:55:16", stringDatetimeTimestamp);

    }

    [Fact]
    public void UIReadCheepTest()
    {
        // Arrange
        var cheep = new Cheep("lana", "hello world", 1695117316);
        var formattedCheep = "lana @ 09/19/23 11:55:16: hello world";
        var output = new StringWriter();
        Console.SetOut(output);

        //Act
        UserInterface.ReadCheep(cheep);

        //Assert
        Assert.Equal(formattedCheep, output.ToString().Trim());

    }

    [Fact]
    public void UIPrintCheepsTest()
    {
        //Arrange
        IEnumerable<Cheep> cheeplist = new List<Cheep> { new("lana", "hello world", 1695117316), new("kram", "Hello friends", 1695117317) };
        var formattedCheeps = "lana @ 09/19/23 11:55:16: hello world\nkram @ 09/19/23 11:55:17: Hello friends";
        var output = new StringWriter();
        Console.SetOut(output);

        //Act
        UserInterface.PrintCheeps(cheeplist);

        //Assert
        Assert.Equal(formattedCheeps, output.ToString().Trim());

    }

}
