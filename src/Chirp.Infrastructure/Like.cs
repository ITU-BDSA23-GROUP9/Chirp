namespace Chirp.Infrastructure
{
    /// <summary>
    /// Represents a like of a cheep in the database.
    /// </summary>
    public class Like
    {
        /// <summary>
        /// Gets or sets the id of a cheep.
        /// </summary>
        public required string LikeId { get; set; }
        /// <summary>
        /// Gets or sets the id of an author.
        /// </summary>
        public string? AuthorId { get; set; }
        /// <summary>
        /// Gets or sets an author of a like.
        /// </summary>
        public required Author Author { get; set; }
        /// <summary>
        /// Gets or sets the id of a cheep.
        /// </summary>
        public string? CheepId { get; set; }
        /// <summary>
        /// Gets or sets the cheep that is liked.
        /// </summary>
        public required Cheep Cheep { get; set; }
    }
}