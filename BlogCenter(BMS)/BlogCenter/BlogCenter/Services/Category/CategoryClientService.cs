using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using System.Net.Http.Headers;
using static BlogCenter.WebAPI.Dtos.RequestDto.GetBlogDto;
using System.Text.Json;
using Microsoft.Extensions.Options;
using BlogCenter.WebAPI.Models.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http.Json;
using static BlogCenter.WebAPI.Dtos.ResponceDto.CategoryDto;
using System.Reflection.Metadata;

namespace BlogCenter.Services.Category
{
    public class CategoryClientService : ICategoryClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _token;
        private string? _url;

        public CategoryClientService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
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
        public async Task<ApiPaginationResponse<GetCategoryDto>> GetCategoriesData(DataManipulationDto dto)
        {
            var queryString = $"?page={dto.PageNumber}&pageSize={dto.PageSize}&searchString={dto.SearchString}&sortString={dto.SortString}&searchTable={dto.SearchTable}&userId={dto.UserId}";
            _url = $"/api/Categories/WithPaginantion{queryString}";
            try
            {
                AddAuthorizationHeader();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                ApiPaginationResponse<GetCategoryDto>? apiResponse = await _httpClient.GetFromJsonAsync<ApiPaginationResponse<GetCategoryDto>>(_url, options);
                if (apiResponse?.Result != null)
                {
                    return apiResponse;
                }
                return new ApiPaginationResponse<GetCategoryDto>();

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return new ApiPaginationResponse<GetCategoryDto>();
            }
        }

        public async Task<Dictionary<bool, string>> AddCategory(AddCategoryDto dto)
        {
            AddAuthorizationHeader();
            _url = $"/api/Categories";
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_url, dto);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<Response<GetCategoryDto>>();
                if (responseData != null && responseData.IsSuccess)
                {
                    return new Dictionary<bool, string> { { true, "Category added successfully." } };
                }
                else return new Dictionary<bool, string> { { false, responseData.ErrorMessages.FirstOrDefault().ToString() } };
            }
            return new Dictionary<bool, string> { { false, "error" } }; ;
        }

        public async Task<Dictionary<bool, string>> UpdateCategory(UpdateCategoryDto dto)
        {
            AddAuthorizationHeader();
            _url = $"/api/Categories";
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_url, dto);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<Response<GetCategoryDto>>();
                if (responseData != null && responseData.IsSuccess)
                {
                    return new Dictionary<bool, string> { { true, "Category updated successfully." } };
                }
                else return new Dictionary<bool, string> { { false, responseData.ErrorMessages.FirstOrDefault().ToString() } };
            }
            return new Dictionary<bool, string> { { false, "error" } }; ;
        }

        public async Task<GetCategoryDto> GetCategoryById(int categoryId)
        {
            AddAuthorizationHeader();
            _url = $"/api/Categories/{categoryId}";
            Response<GetCategoryDto>? response = await _httpClient.GetFromJsonAsync<Response<GetCategoryDto>>(_url);
            if (response.IsSuccess)
            {
                return response.ResultOne;
            }
            return new GetCategoryDto();
        }

        public async Task<Dictionary<bool, string>> DeleteCategory(int categoryId)
        {
            AddAuthorizationHeader();
            _url = $"/api/Categories/{categoryId}";
            Response<GetCategoryDto>? response = await _httpClient.DeleteFromJsonAsync<Response<GetCategoryDto>>(_url);
            if (response != null && response.IsSuccess)
            {
                return new Dictionary<bool, string> { { true, "Category deleted successfully." } };
            }
            else return new Dictionary<bool, string> { { false, response.ErrorMessages.FirstOrDefault().ToString() } };
        }
    }
}
