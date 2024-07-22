using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Dtos.Mapper
{
    public static class BlogsCategoryMapper
    {
        public static BlogsCategory ToBlogsCategory(this BlogDto blogDto)
        {
            if (blogDto is null)
            {
                throw new ArgumentNullException(nameof(blogDto));
            }
            BlogsCategory blogsCategory = new();
            List<BlogsCategory> blogsCategories = new List<BlogsCategory>();
            foreach(var category in blogDto.Categories)
            {
                
                blogsCategories.Add(blogsCategory);
            }
            return blogsCategory;
        }
    }
}
