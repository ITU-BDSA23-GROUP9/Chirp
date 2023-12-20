namespace Chirp.Infrastructure
{
    public class Like
    {
        public required string LikeId { get; set; }
        public string? AuthorId { get; set; }
        public required Author Author { get; set; }
        public string? CheepId { get; set; }

        public required Cheep Cheep { get; set; }
    }
}