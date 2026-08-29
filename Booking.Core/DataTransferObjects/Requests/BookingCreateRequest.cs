namespace Booking.Core.DataTransferObjects.Requests
{
    public class BookingCreateRequest
    {
        public Guid RoomId { get; init; }

        public DateTime StartTime { get; init; }
        public TimeSpan Duration { get; init; } // Тривалість бронювання (наприклад, 2 години)

        public List<Guid> SelectedServiceIds { get; init; } = new();
    }
}
