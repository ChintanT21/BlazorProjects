using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Dtos;
using static BlogCenter.WebAPI.Dtos.ResponceDto.CategoryDto;
using static BlogCenter.WebAPI.Dtos.ResponceDto.UserDto;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BlogCenter.Services.User
{
    public class UserClientService : IUserClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _token;
        private string? _url;

        public UserClientService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
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
        public async Task<Dictionary<bool, string>> AddUser(AddUserDto dto)
        {
            AddAuthorizationHeader();
            _url = $"/api/Users";
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_url, dto);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<Response<GetUserDto>>();
                if (responseData != null && responseData.IsSuccess)
                {
                    return new Dictionary<bool, string> { { true, "Users added successfully." } };
                }
                else return new Dictionary<bool, string> { { false, responseData.ErrorMessages.FirstOrDefault().ToString() } };
            }
            return new Dictionary<bool, string> { { false, "error" } }; ;
        }

        public async Task<Dictionary<bool, string>> DeleteUser(long userId)
        {
            throw new NotImplementedException();
        }

        public async Task<GetUserDto> GetUserById(long userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiPaginationResponse<GetUserDto>> GetUsersData(DataManipulationDto dto)
        {
            var queryString = $"?page={dto.PageNumber}&pageSize={dto.PageSize}&searchString={dto.SearchString}&sortString={dto.SortString}&searchTable={dto.SearchTable}&userId={dto.UserId}";
            _url = $"/api/Users/WithPaginantion{queryString}";
            try
            {
                AddAuthorizationHeader();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                ApiPaginationResponse<GetUserDto>? apiResponse = await _httpClient.GetFromJsonAsync<ApiPaginationResponse<GetUserDto>>(_url, options);
                if (apiResponse?.Result != null)
                {
                    return apiResponse;
                }
                return new ApiPaginationResponse<GetUserDto>();

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return new ApiPaginationResponse<GetUserDto>();
            }
        }

        public async Task<Dictionary<bool, string>> UpdateUser(UpdateUserDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
