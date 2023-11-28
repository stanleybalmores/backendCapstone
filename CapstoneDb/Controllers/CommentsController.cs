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
        private readonly UserRepository _userRepository;
        private readonly CommentRepository _commentRepository;
        private readonly PostRepository _postRepository;

        public CommentsController(CommentRepository commentRepository, PostRepository postRepository, UserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
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
        public ActionResult<IEnumerable<Comment>> GetCommentsPerPost(int postId)
        {
            try
            {
                var post = _commentRepository.GetCommentsPerPost(postId);
                return Ok(post);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving comment: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving comments from the specific post.");
            }
        }


        // POST: api/comments
        [HttpPost]
        public ActionResult<Comment> PostComment([FromBody] CommentDTO commentDTO)
        {

            if (_postRepository.GetPostById(commentDTO.PostId) == null)
            {
                return BadRequest(new { result = "post_doesnt_exist" });
            }

            var newComment = new Comment()
            {
                CommentContent = commentDTO.CommentContent,
                PostId = commentDTO.PostId,
                DateCommented = DateTime.Now,
                CommenterId = commentDTO.CommenterId
            };

            _commentRepository.InsertComment(newComment);
            return Ok(new { result = "added" });
        }

        [HttpPut]
        public IActionResult PutComment([FromBody] CommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("invalid_comment");
            }

            Comment? editComment = _commentRepository.GetCommentById(commentDTO.Id);

            if (editComment == null)
            {
                return BadRequest(new { result = "comment_doesnt_exist" });
            }

            if (commentDTO.CommenterId == editComment.CommenterId)
            {
                return BadRequest(new { result = "user_doesnt_have_rights_to_edit" });
            }

            editComment.CommentContent = commentDTO.CommentContent;

            _commentRepository.UpdateComment(editComment);

            return Ok(new { result = "comment_updated" });
        }

        [HttpDelete]
        public IActionResult DeleteComment(CommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("invalid_comment");
            }

            var commentDelete = _commentRepository.GetCommentById(commentDTO.Id);

            if (commentDelete == null) // binago from != to ==
            {
                return BadRequest(new { result = "comment_doesnt_exist" });
            }

            _commentRepository.DeleteComment(commentDelete);

            return Ok(new { result = "comment_deleted" });
        }
    }
}
