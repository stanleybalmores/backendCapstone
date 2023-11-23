/*using CapstoneDb.Models;
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
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsPerPost(int userId, int postId)
        {
            var post = _postRepository.GetPostByUserById(userId, postId);

            try
            {
                //List<Comment> comments = post.Comments.ToList();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving comment: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }

        // GET: api/post/comments/{postId}/{commentId}
        [HttpGet("{postId}/{postCommentId}")]
        public async Task<ActionResult<Comment>> GetComment(int userId, int postId, int postCommentId)
        {
            try
            {
                var comment = _commentRepository.GetCommentByPostById(userId, postId, postCommentId);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving a comment: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving a comment.");
            }
        }

        // POST: api/comments
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment([FromBody] Comment comment )
        {
            var postId = comment.PostId; //Needs to submit a valid postId inside the new Comment
            _commentRepository.InsertCommentByPost(postId, comment);
            return Ok(new { result = "added" });
        }

        // PUT: api/comments/1
        [HttpPut("{postId}/{postCommentId}")]
        public async Task<IActionResult> PutComment(int userId, int postId, int postCommentId, Comment updatedComment)
        {
            var comment = _commentRepository.GetCommentByPostById(userId, postId, postCommentId);

            if (comment == null) // binago from != to ==
            {
                return BadRequest(new { result = "comment_doesnt_exist" });
            }

            comment.Body = updatedComment.Body;


            _commentRepository.UpdateComment(comment);
            return Ok(new { result = "updated comment" });
        }
    }
}
*/