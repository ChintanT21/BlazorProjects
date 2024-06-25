using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DemoSMSBlazorWebApi.Entity.Dtos;

[Table("Student")]
public partial class StudentDto
{

    public int? StudentId { get; set; } = 0;

    [Required(ErrorMessage = "First Name is required.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name is required.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }= string.Empty;

    [Required(ErrorMessage = "Gender is required.")]
    public string Gender { get; set; } = string.Empty;
}
