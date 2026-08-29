namespace Booking.Core.DataTransferObjects.Requests
{
    public class RoomUpdateRequest
    {
        public decimal? NewBasePrice { get; init; }
        public Guid? ServiceIdToAdd { get; init; }
        public Guid? ServiceIdToRemove { get; init; }
    }
}
