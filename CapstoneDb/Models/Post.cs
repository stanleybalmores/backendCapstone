using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace CapstoneDb.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime DatePosted { get; set; } = DateTime.Now;
        public int PosterId { get; set; }
        public User? Poster { get; set; }

    }


    public class PostDTO
    {
        public string? Title { get; set; }

        [Required]
        public string Content { get; set; } = null!;
        public int PosterId { get; set; }
    }

    public class PostViewResponse
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DatePosted { get; set; }        

    }
}

