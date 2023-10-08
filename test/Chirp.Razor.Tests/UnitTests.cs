namespace Chirp.Razor.Tests;


public class UnitTests : IDisposable
{
        // After realizing we needed 1) a singleton for testing purposes and 2) to dispose of the 
        // singleton after the tests were finished to avoid the "UNIQUE constraint failed" error,
        // ChatGPT was asked on how to actually create that in a testing environment. 
        // Hence, the following lines (until line 28) was taken from ChatGPT.
        private WebApplication app;
        private DBFacade facade;

        public UnitTests() {
            var builder = WebApplication.CreateBuilder(new string[] { });

            builder.Services.AddRazorPages();
            builder.Services.AddSingleton<ICheepService, CheepService>();
            builder.Services.AddSingleton<DBFacade>();

            app = builder.Build();

            facade = app.Services.GetRequiredService<DBFacade>();
        }

        public void Dispose()
        {
            app.DisposeAsync();
        }

    [Theory]
    [InlineData("notSet", "notSet")]
    [InlineData("CHIRPDBPATH", "./mychirp.db")]
    public void CheepServiceGetCheepsTest(string environmentVariable, string value)
    {
        // Arrange
        Environment.SetEnvironmentVariable(environmentVariable, value);
        CheepService cheepService = new CheepService(facade);

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
        CheepService cheepService = new CheepService(facade);

        // Act
        var cheeps = cheepService.GetCheepsFromAuthor("Helge");

        // Assert 
        Assert.Equal("Helge", cheeps[0].Author);
        Assert.Equal("Hello, BDSA students!", cheeps[0].Message);
        Assert.Equal("08/01/23 12:16:48", cheeps[0].Timestamp);
        Assert.Equal(1, cheeps.Count);
    }
}