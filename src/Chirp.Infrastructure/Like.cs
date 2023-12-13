namespace Chirp.Infrastructure
{
    public class Like
    {
        public required string LikeId { get; set; }
        public required Author Author { get; set; }
        public required Cheep Cheep { get; set; }
    }
}