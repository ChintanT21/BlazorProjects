using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DemoSMSBlazorWebApi.Entity.Models;

[Table("Student")]
public partial class Student
{
    [Key]
    public int StudentId { get; set; }

    [Column(TypeName = "character varying")]
    [Required(ErrorMessage = "First Name is required.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name is required.")]
    [Column(TypeName = "character varying")]
    public string LastName { get; set; } = string.Empty;

    public int? CourceId { get; set; }

    public int? Age { get; set; }

    [Column(TypeName = "character varying")]
    [Required(ErrorMessage = "Email is required.")]
    public string? Email { get; set; }= string.Empty;

    [Column(TypeName = "character varying")]
    [Required(ErrorMessage = "Email is required.")]
    public string? Gender { get; set; } = string.Empty;

    [Column(TypeName = "character varying")]
    public string? Cource { get; set; }

    [Column(TypeName = "character varying")]
    public string? Grade { get; set; }

    [ForeignKey("CourceId")]
    [InverseProperty("Students")]
    public virtual Cource? CourceNavigation { get; set; }
}
