using System.ComponentModel.DataAnnotations;

namespace TestingLogin.Models
{
    public class Login
    {
        public string Username { get; set; } = string.Empty;
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$",
         ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
        [StringLength(100, MinimumLength = 6,
         ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = string.Empty;
    }
}
