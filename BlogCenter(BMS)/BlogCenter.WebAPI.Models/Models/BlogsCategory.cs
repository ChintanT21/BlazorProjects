using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlogCenter.WebAPI.Models.Models;

[Table("blogs_categories")]
public partial class BlogsCategory
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("blog_id")]
    public long BlogId { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("updated_date", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedDate { get; set; }

    [Column("created_by")]
    public long CreatedBy { get; set; }

    [Column("updated_by")]
    public long? UpdatedBy { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("BlogId")]
    [InverseProperty("BlogsCategories")]
    public virtual Blog Blog { get; set; } = null!;

    [ForeignKey("CategoryId")]
    [InverseProperty("BlogsCategories")]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("BlogsCategoryCreatedByNavigations")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("UpdatedBy")]
    [InverseProperty("BlogsCategoryUpdatedByNavigations")]
    public virtual User? UpdatedByNavigation { get; set; }
}
