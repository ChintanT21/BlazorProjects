using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.Mapper;
using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text.Json;
using static BlogCenter.WebAPI.Dtos.RequestDto.GetBlogDto;

namespace BlogCenter.Blazor.Services
{
    public class ClientService : IClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _token;
        private string? _url;

        public ClientService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _token = _httpContextAccessor.HttpContext.Request.Cookies["authToken"]?.ToString() ?? string.Empty;
        }
        private void AddAuthorizationHeader()
        {
            if (!string.IsNullOrEmpty(_token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            }
        }
        public async Task<BlogTableDto> GetBlogData(DataManipulationDto dto)
        {
            var queryString = $"?page={dto.PageNumber}&pageSize={dto.PageSize}&searchString={dto.SearchString}&searchTable={dto.SearchTable}&sortString={dto.SortString}&userId={dto.UserId}";
            _url = $"/api/Blogs{queryString}";


            try
            {
                AddAuthorizationHeader();

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
            AddAuthorizationHeader();
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
            Response<GetCategoryDto>? responce =  await _httpClient.GetFromJsonAsync<Response<GetCategoryDto>>(_url);
            return responce.ResultList;
        }

        public async Task<GetBlog> GetOneBlog(long blogId)
        {
            AddAuthorizationHeader();
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
            AddAuthorizationHeader();
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
            var response = await _httpClient.PostAsJsonAsync(_url,blogId);
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
    }
}
