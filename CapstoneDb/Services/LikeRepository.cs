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

        public Like? GetLikeByUserIdAndPostId(int PostId, int UserId)
        {
            return _dbContext.Likes.Where(x => x.PostId == PostId).FirstOrDefault(x => x.UserId == UserId);
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
