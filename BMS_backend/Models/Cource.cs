using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS_backend.Models;

[Table("Cource")]
public partial class Cource
{
    [Key]
    public int CourceId { get; set; }

    [Column(TypeName = "character varying")]
    public string? CourceName { get; set; }

    [InverseProperty("CourceNavigation")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
