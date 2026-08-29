using Booking.Core.Interfaces.Repositories;
using Booking.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Postgres.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<BookingRecord?> GetByIdAsync(Guid id)
        {
            return await _context.BookingRecords
                .Include(b => b.Room)
                .Include(b => b.SelectedServices)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<BookingRecord>> GetAllAsync()
        {
            return await _context.BookingRecords.ToListAsync();
        }

        public async Task<List<BookingRecord>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.BookingRecords
                .AsNoTracking()
                .Include(b => b.Room)
                .Where(b => b.StartTime >= startDate && b.EndTime <= endDate)
                .ToListAsync();
        }

        public async Task<bool> IsRoomBookedAsync(Guid roomId, DateTime startTime, DateTime endTime)
        {
            return await _context.BookingRecords
                .AnyAsync(b => b.RoomId == roomId &&
                               b.StartTime < endTime &&
                               b.EndTime > startTime);
        }

        public async Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.BookingRecords
                .Where(b => b.StartTime >= startDate && b.EndTime <= endDate)
                .SumAsync(b => b.TotalPrice);
        }

        public async Task<Dictionary<string, decimal>> GetRevenueByRoomAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.BookingRecords
                .Where(b => b.StartTime >= startDate && b.EndTime <= endDate && b.Room != null)
                .GroupBy(b => b.Room!.Name)
                .Select(g => new
                {
                    RoomName = g.Key,
                    Total = g.Sum(b => b.TotalPrice)
                })
                .ToDictionaryAsync(x => x.RoomName, x => x.Total);
        }

        public async Task<Dictionary<string, double>> GetRoomOccupancyHoursAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.BookingRecords
                .Where(b => b.StartTime >= startDate && b.EndTime <= endDate && b.Room != null)
                .GroupBy(b => b.Room!.Name)
                .Select(g => new
                {
                    RoomName = g.Key,
                    TotalHours = g.Sum(b => (b.EndTime - b.StartTime).TotalHours)
                })
                .ToDictionaryAsync(x => x.RoomName, x => x.TotalHours);
        }

        public void Add(BookingRecord booking) => _context.BookingRecords.Add(booking);
        public void Delete(BookingRecord booking) => _context.BookingRecords.Remove(booking);
    }
}
