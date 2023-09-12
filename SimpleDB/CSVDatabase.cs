
namespace SimpleDB;

sealed class CSVDatabase : IDatabaseRepository<CSVDatabase>
{
    public IEnumerable<CSVDatabase> Read(int? limit = null)
    {
        throw new NotImplementedException();
    }

    public void Store(CSVDatabase record)
    {
        throw new NotImplementedException();
    }
}