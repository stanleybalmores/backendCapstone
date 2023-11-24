using CapstoneDb.Data;
using CapstoneDb.Models;
using Microsoft.EntityFrameworkCore;

namespace CapstoneDb.Services
{
    public class PostRepository
    {
        private readonly CapstoneDbContext _dbContext;

        public PostRepository(CapstoneDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Post> GetAllPosts()
        {
            return _dbContext.Posts.ToList();
        }

        public Post? GetPostById(int id)
        {
            return _dbContext.Posts.FirstOrDefault(p => p.Id == id);
        }

        public List<Post> GetAllPostsByUserId(int userId)
        {
            return _dbContext.Posts.Where(p => p.PosterId == userId).ToList();
        }

        public void InsertPost(Post post)
        {
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            _dbContext.Posts.Update(post);
            _dbContext.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();
        }

    }
}