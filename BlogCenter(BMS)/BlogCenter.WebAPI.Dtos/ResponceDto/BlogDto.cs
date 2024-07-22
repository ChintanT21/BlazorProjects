using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogCenter.WebAPI.Dtos.ResponceDto
{
    public class BlogDto
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? AdminComment { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }= DateTime.Now;
        public long? CreatedBy { get; set; } 
        public long? UpdatedBy { get; set; }
        public short? Status { get; set; }
        public List<int>? Categories { get; set; }
    }
}
