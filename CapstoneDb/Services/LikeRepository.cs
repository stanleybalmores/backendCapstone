using CapstoneDb.Data;
using CapstoneDb.Models;
using Microsoft.EntityFrameworkCore;

namespace CapstoneDb.Services
{
    public class LikeRepository
    {
        private readonly CapstoneDbContext _dbContext;
        public LikeRepository(CapstoneDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Like? GetLikeById(int id)
        {
            return _dbContext.Likes.FirstOrDefault(x => x.Id == id);
        }

        public List<Like>? GetLikesPerPost(int postId)
        {
            return _dbContext.Likes.Where(x => x.PostId == postId).ToList();
        }
        public void InsertLike(Like like)
        {
            _dbContext.Likes.Add(like);
            _dbContext.SaveChanges();
        }

        public void UnlikePost(Like like)
        {
            _dbContext.Likes.Remove(like);
            _dbContext.SaveChanges();
        }
    }
}
