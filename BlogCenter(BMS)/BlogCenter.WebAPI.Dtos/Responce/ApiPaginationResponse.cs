using System.Net;

namespace BMS.Server.ViewModels
{
    public class ApiPaginationResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
