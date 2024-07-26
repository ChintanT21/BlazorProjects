using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static BlogCenter.WebAPI.Dtos.Enums.Enums;
using static BlogCenter.WebAPI.Dtos.RequestDto.GetBlogDto;

namespace BlogCenter.WebAPI.Dtos.Mapper
{
    public static class BlogMapper
    {
        public static GetBlog ToGetBlogFromBlogDto(this BlogDto updateBlogDto)//not nececcery
        {
            return new GetBlog
            {
                Id = updateBlogDto.BlogId,
                Title = updateBlogDto.Title ?? string.Empty,
                Content = updateBlogDto.Content ?? string.Empty,
                AdminComment = updateBlogDto.AdminComment,
                CreatedDate = updateBlogDto.CreatedDate ?? DateTime.Now,
                UpdatedDate = updateBlogDto.UpdatedDate,
                CreatedBy = updateBlogDto.CreatedBy ?? 0,
                UpdatedBy = updateBlogDto.UpdatedBy,
                Status = updateBlogDto.Status ?? 0,
                BlogsCategories = updateBlogDto.Categories?
                    .Select(categoryId => new GetBlogsCategory
                    {
                        CategoryId = categoryId,
                        BlogId = updateBlogDto.BlogId,
                        CreatedDate = DateTime.Now,
                        CreatedBy = updateBlogDto.CreatedBy ?? 0,
                        IsDeleted = false
                    }).ToList() ?? new List<GetBlogsCategory>()
            };
        }
        public static BlogDto ToBlogDto(this GetBlog getBlog)
        {
            return new BlogDto
            {
                BlogId = getBlog.Id,
                Title = getBlog.Title,
                Content = getBlog.Content,
                AdminComment = getBlog.AdminComment,
                CreatedDate = getBlog.CreatedDate,
                UpdatedDate = getBlog.UpdatedDate,
                CreatedBy = getBlog.CreatedBy,
                UpdatedBy = getBlog.UpdatedBy,
                Status = getBlog.Status,
                Categories = getBlog.BlogsCategories.Select(c => c.CategoryId).ToList()
            };
        }
        public static List<GetBlog> ToGetBlogList(this List<Blog> blogs)
        {
            List<GetBlog> getblog = new List<GetBlog>();

            foreach (var blogDto in blogs)
            {
                GetBlog blog = new GetBlog
                {
                    Id=blogDto.Id,
                    Title = blogDto.Title,
                    Content = blogDto.Content,
                    AdminComment = blogDto.AdminComment,
                    CreatedBy = blogDto.CreatedBy,
                    UpdatedBy = blogDto.UpdatedBy,
                    CreatedDate = blogDto.CreatedDate,
                    UpdatedDate = blogDto.UpdatedDate,
                    Status = blogDto.Status,
                    StatusName = Enum.IsDefined(typeof(BlogStatus), (int)blogDto.Status) ? (BlogStatus)(int)blogDto.Status : throw new ArgumentOutOfRangeException(nameof(blogDto.Status), "Invalid status value"),
                    BlogsCategories = ToGetBlogsCategories(blogDto.BlogsCategories)
                };

                getblog.Add(blog);
            }
            return getblog;
        }

        private static List<GetBlogsCategory> ToGetBlogsCategories(ICollection<BlogsCategory> blogsCategories)
        {
            List<GetBlogsCategory> getBlogsCategories = new List<GetBlogsCategory>();
            foreach (var blogDto in blogsCategories)
            {
                GetBlogsCategory blog = new GetBlogsCategory
                {
                    CategoryId = blogDto.CategoryId,
                };
                getBlogsCategories.Add(blog);

            }
            return getBlogsCategories;
        }

        public static GetBlog ToGetBlogDto(this Blog blogDto)
        {
            return new GetBlog
            {
                Id = blogDto.Id,
                Title = blogDto.Title,
                Content = blogDto.Content,
                AdminComment = blogDto.AdminComment,
                CreatedBy = blogDto.CreatedBy,
                UpdatedBy = blogDto.UpdatedBy == 0 ? null : blogDto.UpdatedBy,
                CreatedDate = DateTime.Now,
                UpdatedDate = blogDto.UpdatedBy == 0 ? null : DateTime.Now,
                Status = blogDto.Status,
                StatusName = Enum.IsDefined(typeof(BlogStatus), (int)blogDto.Status) ? (BlogStatus)(int)blogDto.Status : throw new ArgumentOutOfRangeException(nameof(blogDto.Status), "Invalid status value"),
                BlogsCategories= ToGetBlogsCategories(blogDto.BlogsCategories),
                BlogsCategoriesIntList =blogDto.BlogsCategories.Select(x => x.CategoryId).ToList(),
            };
        }
        public static Blog AddDtoToBlog(this AddBlogDto addBlogDto)
        {
            return new Blog
            {
                Title = addBlogDto.Title,
                Content = addBlogDto.Content,
                CreatedBy = addBlogDto.CreatedBy,
                CreatedDate = DateTime.Now,
            };
        }
        public static Blog ToBlog(this BlogDto blogDto)
        {
            return new Blog
            {
                Title = blogDto.Title,
                Content = blogDto.Content,
                AdminComment = blogDto.AdminComment,
                CreatedBy = blogDto.CreatedBy ?? 1,
                UpdatedBy = blogDto.UpdatedBy == 0 ? null : blogDto.UpdatedBy,
                CreatedDate = DateTime.Now,
                UpdatedDate = blogDto.UpdatedBy == 0 ? null : DateTime.Now,
                Status = blogDto.Status ?? 1,
            };
        }

        public static BlogDto ToBlogDto(this Models.Models.Blog blog)
        {
            return new BlogDto
            {
                Title = blog.Title,
                Content = blog.Content,
                AdminComment = blog.AdminComment,
                CreatedBy = blog.CreatedBy,
                UpdatedBy = blog.UpdatedBy,
                CreatedDate = DateTime.Now,
                UpdatedDate = blog.UpdatedBy == 0 ? null : DateTime.Now,
                Status = blog.Status,
            };
        }

        public static List<Blog> MapToListOfBlogs(object obj)
        {
            if (obj == null)
                return null;

            List<Blog> blogs = new List<Blog>();

            // Assuming obj is a list or collection of objects representing Blog entities
            if (obj is List<object> blogObjects)
            {
                foreach (var blogObj in blogObjects)
                {
                    Blog blog = MapToBlog(blogObj);
                    blogs.Add(blog);
                }
            }

            return blogs;
        }

        public static Blog MapToBlog(object obj)
        {
            if (obj == null)
                return null;

            // Assuming obj is a dictionary or another structured data source
            var properties = obj.GetType().GetProperties();

            Blog blog = new Blog();

            foreach (var prop in properties)
            {
                switch (prop.Name)
                {
                    case "Id":
                        blog.Id = Convert.ToInt64(prop.GetValue(obj));
                        break;
                    case "Title":
                        blog.Title = Convert.ToString(prop.GetValue(obj));
                        break;
                    case "Content":
                        blog.Content = Convert.ToString(prop.GetValue(obj));
                        break;
                    case "AdminComment":
                        blog.AdminComment = Convert.ToString(prop.GetValue(obj));
                        break;
                    case "CreatedDate":
                        blog.CreatedDate = Convert.ToDateTime(prop.GetValue(obj));
                        break;
                    case "UpdatedDate":
                        blog.UpdatedDate = (DateTime?)prop.GetValue(obj);
                        break;
                    case "CreatedBy":
                        blog.CreatedBy = Convert.ToInt64(prop.GetValue(obj));
                        break;
                    case "UpdatedBy":
                        blog.UpdatedBy = (long?)prop.GetValue(obj);
                        break;
                    case "Status":
                        blog.Status = Convert.ToInt16(prop.GetValue(obj));
                        break;
                        // Handle other properties as needed
                }
            }

            return blog;
        }
        public static List<Blog> MapJsonToBlogs1(string jsonString)
        {
            try
            {
                // Parse JSON string to JsonDocument
                using (JsonDocument jsonDoc = JsonDocument.Parse(jsonString))
                {
                    // Access root JsonElement
                    JsonElement root = jsonDoc.RootElement;

                    // Assuming the root is an object with a property named "$values" which is an array
                    if (root.TryGetProperty("$values", out JsonElement valuesElement) && valuesElement.ValueKind == JsonValueKind.Array)
                    {
                        List<Blog> blogs = new List<Blog>();

                        // Iterate over each element in the array
                        foreach (var value in valuesElement.EnumerateArray())
                        {
                            var blog = new Blog
                            {
                                Id = value.GetProperty("id").GetInt64(),
                                Title = value.GetProperty("title").GetString(),
                                Content = value.GetProperty("content").GetString(),
                                AdminComment = value.TryGetProperty("adminComment", out var adminCommentElement) ? adminCommentElement.GetString() : "ADMINCOMMENT",
                                CreatedDate = DateTime.Parse(value.GetProperty("createdDate").GetString()),
                                UpdatedDate = value.TryGetProperty("updatedDate", out var updatedDateElement) ? (DateTime?)DateTime.Parse(updatedDateElement.GetString()) : DateTime.Parse("2024-07-19T09:55:41"),
                                CreatedBy = value.GetProperty("createdBy").GetInt64(),
                                UpdatedBy = value.TryGetProperty("updatedBy", out var updatedByElement) ? (long?)updatedByElement.GetInt64() : 100,
                                Status = value.GetProperty("status").GetInt16(),
                                BlogsCategories = new List<BlogsCategory>(), // Initialize empty collection or parse if needed
                                CreatedByNavigation = null!, // Initialize appropriately if needed
                                UpdatedByNavigation = null // Initialize appropriately if needed
                            };
                            if (value.TryGetProperty("blogsCategories", out var blogsCategoriesElement) && blogsCategoriesElement.TryGetProperty("$values", out var blogsCategoriesValues))
                            {
                                foreach (var categoryElement in blogsCategoriesValues.EnumerateArray())
                                {
                                    var blogsCategory = new BlogsCategory
                                    {
                                        Id = categoryElement.GetProperty("id").GetInt64(),
                                        BlogId = categoryElement.GetProperty("blogId").GetInt64(),
                                        CategoryId = categoryElement.GetProperty("categoryId").GetInt32(),
                                        CreatedDate = DateTime.Parse(categoryElement.GetProperty("createdDate").GetString()),
                                        UpdatedDate = categoryElement.TryGetProperty("updatedDate", out var categoryUpdatedDateElement) && !string.IsNullOrEmpty(categoryUpdatedDateElement.GetString()) ? (DateTime?)DateTime.Parse(categoryUpdatedDateElement.GetString()) : DateTime.Parse("2024-07-19T09:55:41"),
                                        CreatedBy = categoryElement.GetProperty("createdBy").GetInt64(),
                                        UpdatedBy = categoryElement.TryGetProperty("updatedBy", out var categoryUpdatedByElement) && !string.IsNullOrEmpty(categoryUpdatedByElement.GetString()) ? (long?)categoryUpdatedByElement.GetInt64() : 100,
                                        IsDeleted = categoryElement.GetProperty("isDeleted").GetBoolean(),
                                        Category = null, // Assuming category details are not present, set accordingly
                                        CreatedByNavigation = null, // Initialize appropriately if needed
                                        UpdatedByNavigation = null // Initialize appropriately if needed
                                    };
                                    blog.BlogsCategories.Add(blogsCategory);
                                }
                            }
                            Console.WriteLine(blog);

                            blogs.Add(blog);
                        }

                        return blogs;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid JSON structure or missing '$values' array.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error mapping JSON to list of Blog entities", ex);
            }
        }
        public static List<GetBlog> MapJsonToBlogs(string jsonString)
        {
            using (JsonDocument jsonDoc = JsonDocument.Parse(jsonString))
            {
                JsonElement root = jsonDoc.RootElement;

                if (root.TryGetProperty("$values", out JsonElement valuesElement) && valuesElement.ValueKind == JsonValueKind.Array)
                {
                    List<GetBlog> blogs = new List<GetBlog>();

                    foreach (var value in valuesElement.EnumerateArray())
                    {
                        var blog = new GetBlog
                        {
                            Id = GetInt64(value, "id"),
                            Title = GetString(value, "title"),
                            Content = GetString(value, "content"),
                            AdminComment = GetString(value, "adminComment"),
                            CreatedDate = GetDateTime(value, "createdDate"),
                            UpdatedDate = GetNullableDateTime(value, "updatedDate"),
                            CreatedBy = GetInt64(value, "createdBy"),
                            UpdatedBy = GetNullableInt64(value, "updatedBy"),
                            Status = GetInt16(value, "status"),
                            StatusName = ConvertToBlogStatus(GetInt16(value, "status")),
                            BlogsCategories = ParseBlogsCategories(value)
                        };

                        blogs.Add(blog);
                    }

                    int totalPages = root.TryGetProperty("TotalPages", out JsonElement totalPagesElement) ? totalPagesElement.GetInt32() : 0;
                    int totalCount = root.TryGetProperty("TotalCount", out JsonElement totalCountElement) ? totalCountElement.GetInt32() : 0;
                    return blogs;
                }
                else
                {
                    throw new ArgumentException("Invalid JSON structure or missing '$values' array.");
                }
            }
        }

        private static List<GetBlogsCategory> ParseBlogsCategories(JsonElement blogElement)
        {
            var categories = new List<GetBlogsCategory>();

            if (blogElement.TryGetProperty("blogsCategories", out var blogsCategoriesElement) &&
                blogsCategoriesElement.TryGetProperty("$values", out var blogsCategoriesValues))
            {
                foreach (var categoryElement in blogsCategoriesValues.EnumerateArray())
                {
                    categories.Add(new GetBlogsCategory
                    {
                        Id = categoryElement.GetProperty("id").GetInt64(),
                        BlogId = categoryElement.GetProperty("blogId").GetInt64(),
                        CategoryId = categoryElement.GetProperty("categoryId").GetInt32(),
                        CreatedDate = DateTime.Parse(categoryElement.GetProperty("createdDate").GetString() ?? "2024-07-19T09:55:41"),
                        UpdatedDate = categoryElement.TryGetProperty("updatedDate", out var categoryUpdatedDateElement) && !string.IsNullOrEmpty(categoryUpdatedDateElement.GetString()) ? (DateTime?)DateTime.Parse(categoryUpdatedDateElement.GetString()) : null,
                        CreatedBy = categoryElement.GetProperty("createdBy").GetInt64(),
                        UpdatedBy = categoryElement.TryGetProperty("updatedBy", out var categoryUpdatedByElement) && !string.IsNullOrEmpty(categoryUpdatedByElement.GetString()) ? (long?)categoryUpdatedByElement.GetInt64() : null,
                        IsDeleted = categoryElement.GetProperty("isDeleted").GetBoolean()
                    });
                }
            }
            return categories;
        }
        private static BlogStatus ConvertToBlogStatus(short statusValueShort)
        {
            int statusValue = (int)statusValueShort;
            return Enum.IsDefined(typeof(BlogStatus), statusValue)
                ? (BlogStatus)statusValue
                : throw new ArgumentOutOfRangeException(nameof(statusValue), "Invalid status value");
        }
        private static long GetInt64(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.Number)
            {
                return prop.GetInt64();
            }
            throw new InvalidOperationException($"Property '{propertyName}' is missing or not a number.");
        }

        private static int GetInt32(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.Number)
            {
                return prop.GetInt32();
            }
            throw new InvalidOperationException($"Property '{propertyName}' is missing or not a number.");
        }

        private static short GetInt16(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.Number)
            {
                return prop.GetInt16();
            }
            throw new InvalidOperationException($"Property '{propertyName}' is missing or not a number.");
        }

        private static string GetString(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.String)
            {
                return prop.GetString() ?? string.Empty;
            }
            throw new InvalidOperationException($"Property '{propertyName}' is missing or not a string.");
        }

        private static bool GetBoolean(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.True || prop.ValueKind == JsonValueKind.False)
            {
                return prop.GetBoolean();
            }
            throw new InvalidOperationException($"Property '{propertyName}' is missing or not a boolean.");
        }

        private static DateTime GetDateTime(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.String)
            {
                return DateTime.Parse(prop.GetString() ?? "2024-07-19T09:55:41");
            }
            throw new InvalidOperationException($"Property '{propertyName}' is missing or not a string.");
        }

        private static DateTime? GetNullableDateTime(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.String)
            {
                var dateString = prop.GetString();
                return !string.IsNullOrEmpty(dateString) ? (DateTime?)DateTime.Parse(dateString) : null;
            }
            return null;
        }

        private static long? GetNullableInt64(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement prop) && prop.ValueKind == JsonValueKind.Number)
            {
                return prop.GetInt64();
            }
            return null;
        }
    }
}
