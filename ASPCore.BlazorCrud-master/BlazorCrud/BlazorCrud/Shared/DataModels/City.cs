using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace BlazorCrud.Shared.DataModels;

[Table("cities")]
public partial class City
{
    [Key]
    [Column("cityid")]
    public int Cityid { get; set; }

    [Column("cityname")]
    [StringLength(80)]
    public string Cityname { get; set; } = null!;
}
