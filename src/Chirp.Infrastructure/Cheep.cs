public class Cheep
{
    public required string CheepId { get; set; }
    public required Author Author { get; set; }
    public required string Text { get; set; }
    public DateTime TimeStamp { get; set; }
}