using System.Net;

namespace BlogCenter.WebAPI.Dtos
{
    public class Response<T> where T : class
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public List<T>? ResultList { get; set; }
        public T ResultOne { get; set; }
    }
}
