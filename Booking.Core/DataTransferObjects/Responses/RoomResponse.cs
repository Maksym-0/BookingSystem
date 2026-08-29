namespace Booking.Core.DataTransferObjects.Responses
{
    public class RoomResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Capacity { get; init; }
        public decimal BasePricePerHour { get; init; }

        public List<ServiceResponse> AvailableServices { get; init; } = new();
    }
}
