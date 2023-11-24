using CapstoneDb.Models;
using CapstoneDb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneDb.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Get all users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            try
            {
                List<User> users = _userRepository.GetAllUsers(); 
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving users: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }

        // Get user by ID
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            try
            {
                var user = _userRepository.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving a user: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving a user.");
            }
        }

        // Register a new user
        [HttpPost("register")]
        public ActionResult<User> Register([FromBody]UserRegisterDTO userRegister)
        {
            var user = _userRepository.GetUserByEmail(userRegister.Email);

            if (user != null)
            {
                return BadRequest(new { result = "user_already_exist" });
            }

            // Create a new User object from the provided details
            var newUser = new User
            {
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                UserName = userRegister.FirstName + userRegister.LastName,
                Email = userRegister.Email,
                Password = PasswordHasher.HashPassword(userRegister.Password),
                Gender = (Gender)userRegister.Gender,
                MobileNumber = userRegister.MobileNumber,
                Birthday = DateTime.Parse(userRegister.Birthday),

            };

            // Add the new user to the list
            _userRepository.InsertUser(newUser);

            // Return the newly created user
            return Ok(new { result = "user_registered_successfully" });
        }

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login([FromBody] UserLoginDTO userLogin)
        {
            try
            {
                var user = _userRepository.GetUserByEmail(userLogin.Email);

                if (user == null)
                {
                    return NotFound(new { result = "user_not_found" });
                }

                if (!PasswordHasher.VerifyPassword(userLogin.Password, user.Password))
                {
                    return BadRequest(new { result = "incorrect_credentials" });
                }

                // Create a custom response object that includes the user information and login status
                var loginResponse = new LoginResponse
                {
                    Email = user.Email,
                    Token = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.FirstName.ToLower() + user.LastName.ToLower()

                };

                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving log in credentials: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving log in credentials.");
            }
        }


    }
}
