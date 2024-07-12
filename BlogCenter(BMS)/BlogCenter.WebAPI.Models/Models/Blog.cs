using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlogCenter.WebAPI.Models.Models;

[Table("blogs")]
public partial class Blog
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("title")]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Column("content")]
    public string Content { get; set; } = null!;

    [Column("admin_comment")]
    [StringLength(255)]
    public string? AdminComment { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("updated_date", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedDate { get; set; }

    [Column("created_by")]
    public long CreatedBy { get; set; }

    [Column("updated_by")]
    public long? UpdatedBy { get; set; }

    [Column("status")]
    public short Status { get; set; }

    [InverseProperty("Blog")]
    public virtual ICollection<BlogsCategory> BlogsCategories { get; set; } = new List<BlogsCategory>();

    [ForeignKey("CreatedBy")]
    [InverseProperty("BlogCreatedByNavigations")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("UpdatedBy")]
    [InverseProperty("BlogUpdatedByNavigations")]
    public virtual User? UpdatedByNavigation { get; set; }
}
