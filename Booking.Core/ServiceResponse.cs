using Booking.Core.Enums;

namespace Booking.Core
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorType Error { get; set; }
        public T? Data { get; set; }

        public ServiceResponse(bool success, string message, ErrorType error, T? data)
        {
            Success = success;
            Message = message;
            Error = error;
            Data = data;
        }
        public ServiceResponse() { }
    }
}
