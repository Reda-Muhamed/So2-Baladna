namespace So2Baladna.API.Helper
{
    public class ApiException : ResponseHandler<string>
    {
        private readonly string details;

        public ApiException(int statusCode, string details ,string data = null, string message = null) : base(statusCode, data, message)
        {
            this.details = details;
        }
    }
}
