using System.Net;

namespace Smile.Core.Application.Models.Error
{
    public class Error
    {
        public string ErrorCode { get; private set; }
        public string Message { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }

        public static Error Build(string errorCode, string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) => new Error
        {
            ErrorCode = errorCode,
            Message = message,
            StatusCode = statusCode
        };
    }
}