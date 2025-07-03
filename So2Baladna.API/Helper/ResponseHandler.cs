using So2Baladna.Core.Entities.Product;

namespace So2Baladna.API.Helper
{
 

    public class ResponseHandler<T>
    {
        private int v;
        private IReadOnlyList<Category> categories;

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ResponseHandler(int statusCode, T data = default, string message = null)
        {
            StatusCode = statusCode;
            Data = data;
            Message = message ?? GetDefaultMessage(statusCode);
        }

        private string GetDefaultMessage(int statusCode)
        {
            // You can extend this as needed
            return statusCode switch
            {
                200 => "OK",
                201 => "Created",
                204 => "No Content",
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                409 => "Conflict",
                429=>"Too many Requests",
                422 => "Unprocessable Entity",
                500 => "Internal Server Error",
                _ => "Unknown Status"
            };
        }
    }

}
