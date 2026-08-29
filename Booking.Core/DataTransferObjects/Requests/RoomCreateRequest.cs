namespace Booking.Core.DataTransferObjects.Requests
{
    public class RoomCreateRequest
    {
        public string Name { get; init; } = string.Empty;
        public int Capacity { get; init; }
        public decimal BasePricePerHour { get; init; }
        public List<Guid> AvailableServiceIds { get; init; } = new();
    }
}
