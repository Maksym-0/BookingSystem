using Booking.Core.DataTransferObjects.Requests;
using Booking.Core.DataTransferObjects.Responses;

namespace Booking.Core.Interfaces.Services
{
    public interface IRoomService
    {
        Task<ServiceResponse<Guid>> CreateRoomAsync(RoomCreateRequest dto);
        Task<ServiceResponse<bool>> UpdateRoomAsync(Guid roomId, RoomUpdateRequest dto);
        Task<ServiceResponse<bool>> DeleteRoomAsync(Guid roomId);
        Task<ServiceResponse<List<RoomResponse>>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime, int capacity);
    }
}
