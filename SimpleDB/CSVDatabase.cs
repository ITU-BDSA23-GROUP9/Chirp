namespace SimpleDB;
using CsvHelper;
using System.Globalization;



public sealed class CSVDatabase<T> : IDatabaseRepository<T>
{
    public IEnumerable<T> Read(int? limit = null)
    {
        using StreamReader reader = new("chirp_cli_db.csv");
        using CsvReader csvReader = new(reader, CultureInfo.InvariantCulture);

        var records = csvReader.GetRecords<T>().ToList();
        return records;
    }

    public void Store(T record)
    {
        throw new NotImplementedException();
    }
}