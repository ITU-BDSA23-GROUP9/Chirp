using SimpleDB;
using UI;

namespace Chirp.CLI.Tests;

public class IntegrationTests
{
    private CSVDatabase<Cheep> SetupDatabase()
    {
        return new CSVDatabase<Cheep>("../../../../testdata/testdatabase.csv");
    }

    [Fact]
    public void UIPrintCheepsIntegrationTest()
    {
        // Arrange
        var databasecheeps = SetupDatabase().Read();

        var formattedCheeps =
        "ropf @ 08/01/23 14:09:20: Hello, BDSA students!\nrnie @ 08/02/23 14:19:38: Welcome to the course!\nrnie @ 08/02/23 14:37:38: I hope you had a good summer.\nropf @ 08/02/23 15:04:47: Cheeping cheeps on Chirp :)";
        var output = new StringWriter();
        Console.SetOut(output);

        //Act
        UserInterface.PrintCheeps(databasecheeps);

        //Assert
        Assert.Equal(formattedCheeps, output.ToString().Trim());





    }
}
