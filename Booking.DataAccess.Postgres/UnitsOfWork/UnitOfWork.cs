using Booking.Core.Interfaces.Repositories;
using Booking.Core.Interfaces.UnitsOfWork;

namespace Booking.DataAccess.Postgres.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingDbContext _context;

        public IRoomRepository Rooms { get; }
        public IServiceRepository Services { get; }
        public IBookingRepository Bookings { get; }

        public UnitOfWork(BookingDbContext context,
            IRoomRepository rooms,
            IServiceRepository services,
            IBookingRepository bookings)
        {
            _context = context;
            Rooms = rooms;
            Services = services;
            Bookings = bookings;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
