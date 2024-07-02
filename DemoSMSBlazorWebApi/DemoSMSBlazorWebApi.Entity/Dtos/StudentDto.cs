using System.ComponentModel.DataAnnotations;

namespace DemoSMSBlazorWebApi.Entity.Dtos;

public partial class StudentDto
{

    public int StudentId { get; set; } = 0;

    [Required(ErrorMessage = "First Name is required.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name is required.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Gender is required.")]
    public string Gender { get; set; } = string.Empty;

    public string Grade { get; set; } = string.Empty;

    public string Cource { get; set; } = string.Empty;

    public int CourceId { get; set; }


}
