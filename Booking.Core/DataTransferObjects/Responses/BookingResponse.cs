namespace Booking.Core.DataTransferObjects.Responses
{
    public class BookingResponse
    {
        public Guid BookingId { get; init; }
        public Guid RoomId { get; init; }
        public DateTime StartTime { get; init; }
        public DateTime EndTime { get; init; }

        public decimal TotalPrice { get; init; }
    }
}
