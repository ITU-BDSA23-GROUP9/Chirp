namespace Chirp.Razor.Tests;


public class UnitTests
{
    [Theory]
    [InlineData("notSet", "notSet")]
    [InlineData("CHIRPDBPATH", "./mychirp.db")]
    public void CheepServiceGetCheepsTest(string environmentVariable, string value)
    {
        // Arrange
        Environment.SetEnvironmentVariable(environmentVariable, value);
        CheepService cheepService = new();

        // Act
        var cheeps = cheepService.GetCheeps();

        // Assert 
        Assert.Equal("Jacqualine Gilcoine", cheeps[0].Author);
        Assert.Equal("They were married in Chicago, with old Smith, and was expected aboard every day; meantime, the two went past me.", cheeps[0].Message);
        Assert.Equal("08/01/23 13:14:37", cheeps[0].Timestamp);
        Assert.Equal(657, cheeps.Count);
    }

    [Theory]
    [InlineData("notSet", "notSet")]
    [InlineData("CHIRPDBPATH", "./mychirp.db")]
    public void CheepServiceGetCheepsFromAuthorTest(string environmentVariable, string value)
    {
        // Arrange
        Environment.SetEnvironmentVariable(environmentVariable, value);
        CheepService cheepService = new();

        // Act
        var cheeps = cheepService.GetCheepsFromAuthor("Helge");

        // Assert 
        Assert.Equal("Helge", cheeps[0].Author);
        Assert.Equal("Hello, BDSA students!", cheeps[0].Message);
        Assert.Equal("08/01/23 12:16:48", cheeps[0].Timestamp);
        Assert.Equal(1, cheeps.Count);
    }
}