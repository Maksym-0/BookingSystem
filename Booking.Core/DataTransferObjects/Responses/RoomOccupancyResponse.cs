namespace Booking.Core.DataTransferObjects.Responses
{
    public class RoomOccupancyResponse
    {
        public Guid RoomId { get; init; }
        public string RoomName { get; init; } = string.Empty;

        // Скільки всього годин зал був заброньований за вказаний період
        public double TotalHoursBooked { get; init; }
    }
}
