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

        public List<Comment>? GetCommentsPerPost(int id)
        {
            return _dbContext.Comments.Where(x => x.PostId == id).ToList();
        }

        public Comment? GetCommentById(int id)
        {
            return _dbContext.Comments.FirstOrDefault(x => x.Id == id);
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
