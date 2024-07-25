using System.ComponentModel.DataAnnotations.Schema;

namespace BlogCenter.WebAPI.Dtos
{
    public class StudentDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Gender { get; set; }
    }
}
