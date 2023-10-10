public class CheepDTO
{
    public string Message { get; set; }
    public string Author { get; set; }
    public string Timestamp { get; set; }

    public CheepDTO(string message, string author, string timestamp)
    {
        Message = message;
        Author = author;
        Timestamp = timestamp;
    }
}