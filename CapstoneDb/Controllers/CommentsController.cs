using CapstoneDb.Models;
using CapstoneDb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneDb.Controllers
{
    [Route("api/comment")]
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
        public ActionResult<IEnumerable<Comment>> GetAllCommentsinDb()
        {
            try
            {
                List<Comment> comments = _commentRepository.GetAllComments();
                return Ok(comments);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving comment: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving comments.");
            }
        }

        [HttpGet("{postId}")]
        public ActionResult<IEnumerable<Comment>> GetCommentsPerPost(int postId) // to confirm frontend kung need ba talaga to?
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
        public ActionResult<Comment> PostComment([FromBody] CommentDTO commentDTO)
        {
            int postId = commentDTO.PostId; //Needs to submit a valid postId inside the new Comment

            Post? postExists = _postRepository.GetPostById(postId);

            if (postExists == null) // binago from != to ==
            {
                return BadRequest(new { result = "post_doesnt_exist" });
            }

            var newComment = new Comment()
            {
                CommentContent = commentDTO.CommentContent,
                PostId = commentDTO.PostId,
                CommenterId = commentDTO.CommenterId
            };

            _commentRepository.InsertComment(newComment);
            return Ok(new { result = "added" });
        }


    }
}
