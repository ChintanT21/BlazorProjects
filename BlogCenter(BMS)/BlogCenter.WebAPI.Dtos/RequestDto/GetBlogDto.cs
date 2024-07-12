namespace BlogCenter.WebAPI.Dtos.RequestDto
{
    public class GetBlogDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? AdminComment { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public short Status { get; set; }
    }
}
