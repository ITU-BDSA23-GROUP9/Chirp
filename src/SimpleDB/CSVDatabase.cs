namespace SimpleDB;
using CsvHelper;
using System.Globalization;

public sealed class CSVDatabase<T> : IDatabaseRepository<T>
{

    private static CSVDatabase<T> instance = null;
    private string path;

    public static CSVDatabase<T> getInstance(string path)
    {
        if (instance == null)
        {
            instance = new CSVDatabase<T>(path);
        }
        return instance;
    }

    private CSVDatabase(string path)
    {
        if (!File.Exists(path))
        {
            path = "./chirp_cli_db.csv";

            if (File.Exists(path))
            {
                this.path = path;
                return;
            }

            File.Create(path).Close();
            File.WriteAllText(path, "Author,Message,Timestamp");
        }

        this.path = path;
    }

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
