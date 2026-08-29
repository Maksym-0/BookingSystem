namespace Booking.Core.DataTransferObjects.Responses
{
    public class ServiceResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public decimal Price { get; init; }
    }
}
