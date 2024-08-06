using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using static BlogCenter.WebAPI.Dtos.RequestDto.GetBlogDto;

namespace BlogCenter.Shared.ClientServices.Blogs
{
    public class BlogClientService : IBlogClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _token;
        private string? _url;

        public BlogClientService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public async Task<BlogTableDto> GetBlogData(DataManipulationDto dto)
        {
            var queryString = $"?page={dto.PageNumber}&pageSize={dto.PageSize}&searchString={dto.SearchString}&searchTable={dto.SearchTable}&sortString={dto.SortString}&userId={dto.UserId}";
            _url = $"/api/Blogs{queryString}";


            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                ApiPaginationResponse<GetBlog>? apiResponse = await _httpClient.GetFromJsonAsync<ApiPaginationResponse<GetBlog>>(_url, options);
                if (apiResponse?.Result != null)
                {
                    BlogTableDto tableDto = new BlogTableDto();
                    tableDto.Blogs = apiResponse.Result;
                    tableDto.TotalPages = apiResponse.TotalPages;
                    tableDto.TotalCount = apiResponse.TotalCount;
                    return tableDto;
                }
                return new BlogTableDto();

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return new BlogTableDto();
            }

        }

        public async Task<object> ValidateCredential(LoginDto loginDto)
        {
            _url = "/api/Auth/login";
            var response = await _httpClient.PostAsJsonAsync(_url, loginDto);
            return response;
        }

        public async Task<bool> CreateBlog(AddBlogDto blog)
        {

            _url = "/api/Blogs";

            var response = await _httpClient.PostAsJsonAsync(_url, blog);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error creating blog: {response.StatusCode}, {errorContent}");
                return false;
            }
        }

        public async Task<List<GetCategoryDto>?> GetAllCategories()
        {
            _url = "/api/Categories";
            Response<GetCategoryDto>? responce = await _httpClient.GetFromJsonAsync<Response<GetCategoryDto>>(_url);
            return responce.ResultList;
        }

        public async Task<GetBlog> GetOneBlog(long blogId)
        {

            _url = $"/api/Blogs/{blogId}";
            Response<GetBlog>? response = await _httpClient.GetFromJsonAsync<Response<GetBlog>>(_url);
            if (response == null)
            {
                return response.ResultOne = new GetBlog();
            }
            return response.ResultOne;
        }

        public async Task<bool> UpdateBlog(BlogDto blog)
        {

            _url = $"/api/Blogs?blogId={blog.BlogId}";
            var response = await _httpClient.PutAsJsonAsync(_url, blog);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error creating blog: {response.StatusCode}, {errorContent}");
                return false;
            }
        }

        public async Task<bool> ChangeStatus(long blogId, int? statusId)
        {
            _url = $"/api/Blogs/statusId/{statusId}?blogId={blogId}";
            var response = await _httpClient.PostAsJsonAsync(_url, blogId);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error creating blog: {response.StatusCode}, {errorContent}");
                return false;
            }
        }

        public async Task<List<GetUserDto>> GetAllUsers()
        {

            _url = $"/api/Users";
            Response<GetUserDto>? response = await _httpClient.GetFromJsonAsync<Response<GetUserDto>>(_url);
            if (response == null)
            {
                return response.ResultList = new List<GetUserDto>();
            }
            return response.ResultList;
        }
    }
}
