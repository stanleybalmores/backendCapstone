using CapstoneDb.Data;
using CapstoneDb.Models;
using Microsoft.EntityFrameworkCore;

namespace CapstoneDb.Services
{
    public class CommentRepository
    {
        private readonly CapstoneDbContext _dbContext;

        public CommentRepository(CapstoneDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Comment> GetAllComments()
        {
            return _dbContext.Comments.ToList();
        }

        public List<Comment> GetAllCommentsByPostId(int postId)
        {
            return _dbContext.Comments.Where(c => c.PostId == postId).ToList();
        }


        public Comment? GetCommentById(int id)
        {
            return _dbContext.Comments.FirstOrDefault(x => x.Id == id);
        }

        public List<Post>? GetPostsByUserId(int userId)
        {
            return _dbContext.Posts.Where(p => p.PosterId == userId).ToList();
        }

        public void InsertComment(Comment comment)
        {
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();
        }

        public void UpdateComment(Comment comment)
        {
            _dbContext.Comments.Update(comment);
            _dbContext.SaveChanges();
        }

        public void DeleteComment(Comment comment)
        {
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();
        }
    }
}
