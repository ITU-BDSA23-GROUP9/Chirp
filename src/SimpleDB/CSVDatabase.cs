namespace SimpleDB;
using CsvHelper;
using System.Globalization;

public sealed class CSVDatabase<T> : IDatabaseRepository<T>
{
    string path = "../../data/chirp_cli_db.csv";
    public IEnumerable<T> Read(int? limit = null)
    {
        using StreamReader reader = new(path);
        using CsvReader csvReader = new(reader, CultureInfo.InvariantCulture);

        var records = csvReader.GetRecords<T>().ToList();
        return records;
    }

    public void Store(T record)
    {
        using StreamWriter writer = new(path, true);
        using CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture);

        csvWriter.WriteRecord(record);
        writer.WriteLine();
    }
}