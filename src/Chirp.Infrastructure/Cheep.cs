public class Cheep
{
    public required Guid CheepId { get; set; }
    public required Guid AuthorId { get; set; }
    public required Author Author { get; set; }
    public required string Text { get; set; }
    public DateTime TimeStamp { get; set; }
}