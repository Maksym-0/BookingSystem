using Booking.Core.Interfaces.Repositories;
using Booking.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Postgres.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly BookingDbContext _context;

        public RoomRepository(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<Room?> GetByIdAsync(Guid id)
        {
            return await _context.Rooms
                .Include(r => r.AvailableServices)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Room>> GetAllAsync()
        {
            return await _context.Rooms
                .Include(r => r.AvailableServices)
                .ToListAsync();
        }

        public async Task<List<Room>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime, int requestedCapacity)
        {
            return await _context.Rooms
                .AsNoTracking()
                .Include(r => r.AvailableServices)
                .Where(r => r.Capacity >= requestedCapacity)
                .Where(r => !r.Bookings.Any(b => b.StartTime < endTime && b.EndTime > startTime))
                .ToListAsync();
        }

        public void Add(Room room) => _context.Rooms.Add(room);
        public void Delete(Room room) => _context.Rooms.Remove(room);
    }
}
