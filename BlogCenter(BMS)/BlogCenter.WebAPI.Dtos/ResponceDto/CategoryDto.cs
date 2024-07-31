using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Dtos.ResponceDto
{
    public class CategoryDto
    {
        public class AddCategoryDto
        {
            [Required(ErrorMessage ="Category Name is Required")]
            public string Name { get; set; } = string.Empty;
        }
        public class UpdateCategoryDto
        {
            public int Id { get; set; }
            public string? Name { get; set; } 
            public bool? IsDeleted { get; set; } = false;
        }
    }
}
