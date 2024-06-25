using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DemoSMSBlazorWebApi.Entity.Dtos;

[Table("Cource")]
public partial class Cource
{
    [Key]
    public int CourceId { get; set; }

    [Column(TypeName = "character varying")]
    public string? CourceName { get; set; }

    [InverseProperty("CourceNavigation")]
    public virtual ICollection<StudentDto> Students { get; set; } = new List<StudentDto>();
}
