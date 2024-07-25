using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Dtos.Mapper
{
    public static class CategoryMapper
    {
        public static List<GetCategoryDto> MapToGetCategoryDtoList(this IEnumerable<Category> categories)
        {
            return categories.Select(MapToGetCategoryDto).ToList();
        }
        public static GetCategoryDto MapToGetCategoryDto(this Category category)
        {
            return new GetCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                CreatedDate = category.CreatedDate,
                UpdatedDate = category.UpdatedDate,
                CreatedBy = category.CreatedBy,
                UpdatedBy = category.UpdatedBy,
                IsDeleted = category.IsDeleted
            };
        }
    }
}
