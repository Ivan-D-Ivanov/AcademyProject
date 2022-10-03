using System.Net;

namespace AcademyProjectModels.Response
{
    public class BaseResponse
    {
        public HttpStatusCode HttpStatusCode { get; init; }

        public string? Message { get; set; }
    }
}
