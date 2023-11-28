using CapstoneDb.Data;
using CapstoneDb.Models;
using CapstoneDb.Services;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneDb.Controllers
{
    [Route("api/likes")]
    [ApiController]
    public class LikesController : ControllerBase
    {

        private readonly LikeRepository _likeRepository;
        private readonly UserRepository _userRepository;
        private readonly PostRepository _postRepository;

        public LikesController(LikeRepository likeRepository, UserRepository userRepository, PostRepository postRepository)
        {
            _likeRepository = likeRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        [HttpGet("{postId}")]
        public ActionResult<IEnumerable<Like>> GetLikesPerPost(int postId)
        {
            try
            {
                var post = _likeRepository.GetLikesPerPost(postId);
                return Ok(post);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving comment: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving likes from the specific post.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Like>> LikePost(LikeDTO likeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("invalid_like");
            }

            User? poster = await _userRepository.GetUserById(likeDTO.UserId);

            if (poster == null)
            {
                return BadRequest("invalid_user_id");
            }

            if (_postRepository.GetPostById(likeDTO.PostId) == null)
            {
                return BadRequest(new { result = "post_doesnt_exist" });
            }

            var newLike = new Like()
            {
                UserId = likeDTO.UserId,
                PostId = likeDTO.PostId
            };

            _likeRepository.InsertLike(newLike);

            return Ok(new { result = "liked" });
        }

        [HttpDelete]
        public ActionResult<Like> UnlikingPost(LikeDTO likeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("invalid_unlike");
            }

            var unlike = _likeRepository.GetLikeByUserIdAndPostId(likeDTO.PostId, likeDTO.UserId);

            if (unlike == null)
            {
                return BadRequest(new { result = "like_doesnt_exist" });
            }

            _likeRepository.UnlikePost(unlike);

            return Ok(new { result = "unliked" });
        }
    }
}
