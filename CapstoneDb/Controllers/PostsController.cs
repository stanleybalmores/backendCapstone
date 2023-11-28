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

        [HttpPost]
        public async Task<IActionResult> PostPost([FromBody] PostDTO postDTO)
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

            // Retrieve all posts from the repository
            List<Post> allPosts = _postRepository.GetAllPosts();

            // Sort the posts based on DatePosted in descending order
            var sortedPosts = allPosts.OrderByDescending(p => p.DatePosted).ToList();

            // Create a list of PostViewResponse objects
            var postResponse = sortedPosts.Select(post => new PostViewResponse
            {
                PostId = post.Id,
                Title = post.Title,
                Content = post.Content,
                DatePosted = DateTime.Now
            }).ToList();

            return Ok(postResponse);
        }
        /*, [FromHeader] Authorization authorization) // needed bearer token for the authorization to be able to get user*/


        // PUT: api/Posts/5/1

        [HttpPut("{postId}")]
        public IActionResult PutPost(int postId, [FromBody] PostDTO updatedPost)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("invalid_post");
            }

            Post? editPost = _postRepository.GetPostById(postId);

            if (editPost == null) // binago from != to ==
            {
                return BadRequest(new { result = "post_doesnt_exist" });
            }

            if (updatedPost.PosterId == editPost.PosterId)
            {
                return BadRequest(new { result = "user_doesnt_have_rights_to_edit" });
            }

            editPost.Title = updatedPost.Title;
            editPost.Content = updatedPost.Content;

            _postRepository.UpdatePost(editPost);
           
            return Ok(new { result = "post_updated" });
        }

        
        // DELETE: api/posts
        [HttpDelete("{postId}")]
        public IActionResult DeletePost(int postId, int userId)
        {

            var postDelete = _postRepository.GetPostById(postId);

            if (postDelete == null) // binago from != to ==
            {
                return BadRequest(new { result = "post_doesnt_exist" });
            }

            if (userId != postDelete.PosterId)
            {
                return BadRequest(new { result = "user_doesnt_have_rights_to_delete" });
            }

            _postRepository.DeletePost(postDelete);

            return Ok(new { result = "post_deleted" });
        }
    }
}
