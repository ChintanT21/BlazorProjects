using System.ComponentModel.DataAnnotations;

namespace BlogCenter.WebAPI.Dtos.ResponceDto
{
    public class AddBlogDto
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = null!;
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "At least Category is required.")]
        public List<int> Categories { get; set; } = null!;
    }
}
