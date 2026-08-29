using Booking.Core.Models;

namespace Booking.Core.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        Task<BookingRecord?> GetByIdAsync(Guid id);
        Task<List<BookingRecord>> GetAllAsync();
        Task<List<BookingRecord>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate);

        // Чи є перетин часу для конкретного залу, щоб не забронювати двічі
        Task<bool> IsRoomBookedAsync(Guid roomId, DateTime startTime, DateTime endTime);

        // Повертає загальну суму за період
        Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate);

        // Повертає словник: Key (Назва кімнати) -> Value (Сума зароблних грошей)
        Task<Dictionary<string, decimal>> GetRevenueByRoomAsync(DateTime startDate, DateTime endDate);

        // Повертає список: Key (Назва кімнати) -> Value (Сума годин бронювання)
        Task<Dictionary<string, double>> GetRoomOccupancyHoursAsync(DateTime startDate, DateTime endDate);

        void Add(BookingRecord booking);
        void Delete(BookingRecord booking);
    }
}
