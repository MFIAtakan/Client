using System.Net;

namespace Client.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public T Result { get; set; }
    }
}
