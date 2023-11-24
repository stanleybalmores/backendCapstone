using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CapstoneDb.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public string MobileNumber { get; set; }
        public DateTime Birthday { get; set; }
       
    }

    public class UserRegisterDTO
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]        
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]        
        public string Birthday { get; set; }

        public int? Gender { get; set; }

        public string? MobileNumber { get; set; }
    }

    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }

    public class LoginResponse
    {
        public int? UserId { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public int? Token { get; set; }
    }


}
