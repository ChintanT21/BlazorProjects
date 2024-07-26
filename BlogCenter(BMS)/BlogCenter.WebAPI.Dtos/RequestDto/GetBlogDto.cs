using static BlogCenter.WebAPI.Dtos.Enums.Enums;

namespace BlogCenter.WebAPI.Dtos.RequestDto
{
    public class GetBlogDto
    {
        public class GetBlog
        {
            public long Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Content { get; set; } = string.Empty;
            public string? AdminComment { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime? UpdatedDate { get; set; }
            public long CreatedBy { get; set; }
            public long? UpdatedBy { get; set; }
            public short Status { get; set; }
            public BlogStatus StatusName { get; set; }
            public List<GetBlogsCategory> BlogsCategories { get; set; } = new();
            public List<int> BlogsCategoriesIntList { get; set; } = new();
            public int TotalCount { get; set; }
            public int TotalPages { get; set; }
        }

        public class GetBlogsCategory
        {
            public long Id { get; set; }
            public long? BlogId { get; set; }
            public int CategoryId { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime? UpdatedDate { get; set; }
            public long CreatedBy { get; set; }
            public long? UpdatedBy { get; set; }
            public bool IsDeleted { get; set; }
        }
    }
}
