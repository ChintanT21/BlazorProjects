using System.ComponentModel.DataAnnotations;

namespace BlogCenter.WebAPI.Dtos.ResponceDto
{
    public class AddBlogDto
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public List<int>? Categories { get; set; }
    }
}
