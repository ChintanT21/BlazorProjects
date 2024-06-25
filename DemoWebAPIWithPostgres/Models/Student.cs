using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPIWithPostgres.Models;

[Table("Student")]
public partial class Student
{
    [Key]
    public int StudentId { get; set; }

    [Column(TypeName = "character varying")]
    public string FirstName { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string LastName { get; set; } = null!;

    public int? CourceId { get; set; }

    public int? Age { get; set; }

    [Column(TypeName = "character varying")]
    public string? Email { get; set; }

    [Column(TypeName = "character varying")]
    public string? Gender { get; set; }

    [Column(TypeName = "character varying")]
    public string? Cource { get; set; }

    [Column(TypeName = "character varying")]
    public string? Grade { get; set; }

    [ForeignKey("CourceId")]
    [InverseProperty("Students")]
    public virtual Cource? CourceNavigation { get; set; }
}
