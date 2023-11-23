using CapstoneDb.Models;
using CapstoneDb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneDb.Controllers
{
    [Route("api/Posts/Comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentRepository _commentRepository;
        private readonly PostRepository _postRepository;

        public CommentsController(CommentRepository commentRepository, PostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        // GET: api/Posts/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllCommentsinDb()
        {
            try
            {
                List<Comment> comments = _commentRepository.GetAllComments();
                return Ok(comments);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving comment: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsPerPost(int postId)
        {
            

            try
            {
                var post = _postRepository.GetCommentsPerPost(postId);
                return Ok(post);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving comment: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }


        // POST: api/comments
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment([FromBody] Comment comment)
        {
            var postId = comment.PostId; //Needs to submit a valid postId inside the new Comment
            _commentRepository.InsertComment(comment);
            return Ok(new { result = "added" });
        }

        
    }
}
