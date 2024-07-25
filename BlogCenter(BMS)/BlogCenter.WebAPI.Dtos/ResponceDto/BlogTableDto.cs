using static BlogCenter.WebAPI.Dtos.RequestDto.GetBlogDto;

namespace BlogCenter.WebAPI.Dtos.ResponceDto
{
    public class BlogTableDto
    {
        public List<GetBlog> Blogs { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

    }
}
