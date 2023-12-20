namespace Chirp.Infrastructure
{
    public class Like
    {
        public required string LikeId { get; set; }
        public string? AuthorId { get; set; }
        public string? CheepId { get; set; }
    }
}