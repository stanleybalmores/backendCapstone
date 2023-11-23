using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapstoneDb.Data;
using CapstoneDb.Models;
using CapstoneDb.Services;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace CapstoneDb.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly CapstoneDbContext _context;
        private readonly PostRepository _postRepository;
        private readonly UserRepository _userRepository;

        public PostsController(CapstoneDbContext context, PostRepository postRepository)
        {
            _context = context;
            _postRepository = postRepository;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            try
            {
                List<Post> posts = _postRepository.GetAllPosts();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving posts: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving posts.");
            }
        }

        // GET: api/Posts/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPostsByUserId(int userId)
        {
            try
            {
                List<Post> posts = _postRepository.GetAllPostsByUserId(userId);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving posts: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving posts.");
            }
        }
        /*// GET: api/Posts/5/1
        [HttpGet("{userId}/{postId}")]
        public async Task<ActionResult<Post>> GetPostsByUserById(int userId, int postId)
        {
            try
            {
                var post = _postRepository.GetPostsByUserById(userId, postId);
                return Ok(post);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving a post: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving a post.");
            }
        }

        // POST: api/Posts/5

        [HttpPost]
        public async Task<ActionResult<Post>> PostPost([FromBody] Post post*//*, [FromHeader] Authorization authorization*//*) // needed bearer token for the authorization to be able to get user
        {
            var userId = post.UserId; //Must input a valid UserId in the Post object
            _postRepository.InsertPostByUser(userId, post);

            return Ok(new { result = "added" });
        }

        // PUT: api/Posts/5/1

        [HttpPut("{userId}/{postId}")]
        public async Task<IActionResult> PutPost(int userId, int postId, Post updatedPost)
        {
            var post = _postRepository.GetPostByUserById(userId, postId);

            if (post == null) // binago from != to ==
            {
                return BadRequest(new { result = "post_doesnt_exist" });
            }

            post.Title = updatedPost.Title;
            post.Content = updatedPost.Content;

           
            _postRepository.UpdatePost(post);           
            return Ok(new { result = "updated" });
        }

        // DELETE: api/Posts/5/1
        [HttpDelete("{userId}/{postId}")]
        public async Task<IActionResult> DeletePost(int userId, int postId)
        {
           var deleteId = _postRepository.GetPostByUserById(userId, postId);
            _postRepository.DeletePost(deleteId);

            return NoContent();
        }*/

        
    }
}
