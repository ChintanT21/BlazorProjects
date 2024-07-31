using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlogCenter.WebAPI.Dtos.ResponceDto.CategoryDto;

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

        public static Category ToCategoryFromAddCategoryDto(this AddCategoryDto dto, long createdBy)
        {
            return new Category
            {
                Name = dto.Name,
                CreatedDate = DateTime.Now,
                CreatedBy = createdBy,
                IsDeleted = false
            };
        }
        public static Category ToCategoryFromUpdateCategoryDto(this UpdateCategoryDto dto, Category category, long updatedBy)
        {
            category.Id = dto.Id;
            if (!string.IsNullOrEmpty(dto.Name))
            {
                category.Name = dto.Name;
            }
            category.UpdatedDate = DateTime.Now;
            category.UpdatedBy = updatedBy;
            category.IsDeleted = dto.IsDeleted !=null? (bool)dto.IsDeleted:false;

            return category;
        }
        public static UpdateCategoryDto ToUpdateCategoryDto(this GetCategoryDto getCategoryDto)
        {
            return new UpdateCategoryDto
            {
                Id = getCategoryDto.Id,
                Name = getCategoryDto.Name,
                IsDeleted = getCategoryDto.IsDeleted
            };
        }
    }
}
