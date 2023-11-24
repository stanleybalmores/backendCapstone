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

        public PostsController(CapstoneDbContext context, UserRepository userRepository, PostRepository postRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        // GET: api/Posts
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetPosts()
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
        [HttpGet("all/{userId}")]
        public ActionResult<IEnumerable<Post>> GetAllPostsByUserId(int userId)
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

        // GET: api/Posts/5/1
        [HttpGet("{postId}")]
        public IActionResult GetPostById(int postId)
        {
            try
            {
                var post = _postRepository.GetPostById(postId);
                return Ok(post);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving a post: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving a post.");
            }
        }

        
        // POST: api/Posts/5

        [HttpPost("{userId}")]
        public async Task<IActionResult> PostPost(int userId, [FromBody] PostDTO postDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("invalid_post");
            }

            User? poster = await _userRepository.GetUserById(postDTO.PosterId);
            if (poster == null)
            {
                return BadRequest("invalid_user_id");
            }

            var newPost = new Post()
            {
                Title = postDTO.Title,
                Content = postDTO.Content,
                DatePosted = DateTime.Now,
                PosterId = poster.Id

            };
            _postRepository.InsertPost(newPost);

            var postResponse = new PostViewResponse
            {
                PostId = newPost.Id,
                Title = newPost.Title,
                Content = newPost.Content,
                DatePosted = DateTime.Now,
            };

            return Ok(postResponse);
        }
        /*, [FromHeader] Authorization authorization) // needed bearer token for the authorization to be able to get user*/

        
        // PUT: api/Posts/5/1

        [HttpPut("{postId}")]
        public IActionResult PutPost(int postId, [FromBody] PostDTO updatedPost)
        {
            Post? editPost = _postRepository.GetPostById(postId);

            if (updatedPost == null) // binago from != to ==
            {
                return BadRequest(new { result = "post_doesnt_exist" });
            }

            editPost.Title = updatedPost.Title;
            editPost.Content = updatedPost.Content;
            editPost.PosterId = updatedPost.PosterId;


            _postRepository.UpdatePost(editPost);
           
            return Ok(new { result = "updated" });
        }

        
        // DELETE: api/Posts/5/1
        [HttpDelete("{postId}")]
        public IActionResult DeletePost(int postId)
        {

            var post = _postRepository.GetPostById(postId);
            _postRepository.DeletePost(post);

            return NoContent();
        }


    }
}
