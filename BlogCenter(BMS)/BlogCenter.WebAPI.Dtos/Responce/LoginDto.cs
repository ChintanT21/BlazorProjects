using System.ComponentModel.DataAnnotations;

namespace BlogCenter.WebAPI.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Please Enter Email.")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter Password.")]
        [StringLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
        public string Password { get; set; }= null!;
    }
}
