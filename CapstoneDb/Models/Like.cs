namespace CapstoneDb.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int PostId { get; set; }
        public Post? Post { get; set; }
        public DateTime? DateLiked { get; set; } = DateTime.Now;
    }

    public class LikeDTO
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
