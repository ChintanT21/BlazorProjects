using DemoWebAPIWithPostgres.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebAPIWithPostgres.Dto
{
    public class AddStudentDto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int? CourceId { get; set; }

        public string? Email { get; set; }

        public string? Gender { get; set; }

        public string? Cource { get; set; }

        public string? Grade { get; set; }

    }
}
