using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Dtos.ResponceDto
{
    public class UserDto
    {
        public class AddUserDto
        {
            [Required(ErrorMessage = "RoleId is required.")]
            public int RoleId { get; set; }
            [Required(ErrorMessage = "First name is required.")]
            [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
            public string FirstName { get; set; } = null!;

            [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
            public string? LastName { get; set; }

            [Required(ErrorMessage = "Email is required.")]
            [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
            [EmailAddress(ErrorMessage = "Invalid email format.")]
            public string Email { get; set; } = null!;

            [StringLength(50, ErrorMessage = "Profile name cannot exceed 50 characters.")]
            public string? ProfileName { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            [StringLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
            [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one letter and one number.")]
            public string Password { get; set; } = null!;

            public long? CreatedBy { get; set; }

            [Required(ErrorMessage = "Status is required.")]
            public short Status { get; set; }
        }


        public class UpdateUserDto
        {
            [Required(ErrorMessage = "UserId is required.")]
            public long UserId { get; set; }
            [Required(ErrorMessage = "RoleId is required.")]
            public int RoleId { get; set; }

            [Required(ErrorMessage = "First name is required.")]
            [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
            public string FirstName { get; set; } = null!;

            [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
            public string? LastName { get; set; }

            [Required(ErrorMessage = "Email is required.")]
            [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
            public string Email { get; set; } = null!;

            [StringLength(50, ErrorMessage = "Profile name cannot exceed 50 characters.")]
            public string? ProfileName { get; set; }

            [Required(ErrorMessage = "Please Enter Password.")]
            [StringLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
            [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one letter, one number, and one special character.")]
            public string? Password { get; set; }

            [Required(ErrorMessage = "Status is required.")]
            public short Status { get; set; }
        }

    }
}
