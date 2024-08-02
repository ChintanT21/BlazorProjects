using System.Net;

namespace BlogCenter.WebAPI.Dtos
{
    public class ApiPaginationResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = false;
        public List<string> ErrorMessages { get; set; }
        public List<T> Result { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
