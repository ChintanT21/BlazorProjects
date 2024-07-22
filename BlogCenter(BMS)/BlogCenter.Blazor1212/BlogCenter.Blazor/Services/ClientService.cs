using BlogCenter.WebAPI.Dtos.Mapper;
using BMS.Client.Dtos;
using BMS.Server.ViewModels;
using System.Net.Http.Headers;
using System.Text.Json;
using static BlogCenter.WebAPI.Dtos.RequestDto.GetBlogDto;

namespace BlogCenter.Blazor.Services
{
    public class ClientService : IClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _token;
        private string _url;

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



        public async Task<List<GetBlog>> GetBlogData()
        {
            _url = "/api/Blog?page=1&pageSize=5&searchString=database&searchTable=content&sortTable=content&userId=1";
            try
            {
                AddAuthorizationHeader();
                using HttpClient client = new HttpClient();
                var responce = await _httpClient.GetAsync(_url);
                ApiPaginationResponse apiResponse = await responce.Content.ReadFromJsonAsync<ApiPaginationResponse>();
                if (apiResponse.Result != null)
                {
                    string JsonResult = JsonSerializer.Serialize(apiResponse.Result);
                    List<GetBlog> blogs = BlogMapper.MapJsonToBlogs(JsonResult);
                    return blogs;

                }
                return null;

            }
            catch (Exception e)
            {
                throw;
            }

        }

        async Task<object> IClientService.ValidateCredential(LoginDto loginDto)
        {
            _url = "/api/Auth/login";
            var response = await _httpClient.PostAsJsonAsync(_url, loginDto);
            return response;
        }
    }
}
