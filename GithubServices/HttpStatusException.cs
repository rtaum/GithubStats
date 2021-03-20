using System;
using System.Net;

namespace GithubServices
{
    public class HttpStatusException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public HttpStatusException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
