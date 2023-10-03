namespace Chirp.Razor.Tests;


public class UnitTests
{
    [Fact]
    public void CheepServiceGetCheepsTest()
    {
        // Arrange
        CheepService cheepService = new (); 

        // Act
        var cheeps = cheepService.GetCheeps();

        // Assert 
        Assert.Equal("Helge", cheeps[0].Author);
        Assert.Equal("Hello, BDSA students!", cheeps[0].Message);
        Assert.Equal("08/01/23 12:16:48", cheeps[0].Timestamp);
        Assert.Equal(2, cheeps.Count);
    }
}