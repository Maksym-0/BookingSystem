using Booking.Core.Models;

namespace Booking.Core.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        Task<Room?> GetByIdAsync(Guid id);
        Task<List<Room>> GetAllAsync();

        Task<List<Room>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime, int requestedCapacity);

        void Add(Room room);
        void Delete(Room room);
    }
}
