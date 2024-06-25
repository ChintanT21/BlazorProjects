using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Shared.DataModels;

[Table("employee")]
public partial class Employee
{
    [Key]
    [Column("employeeid")]
    public int Employeeid { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("city")]
    [StringLength(255)]
    public string City { get; set; } = null!;

    [Column("department")]
    [StringLength(255)]
    public string Department { get; set; } = null!;

    [Column("gender")]
    [StringLength(10)]
    public string Gender { get; set; } = null!;
}
