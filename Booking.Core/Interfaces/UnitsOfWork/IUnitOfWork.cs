using Booking.Core.Interfaces.Repositories;

namespace Booking.Core.Interfaces.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRoomRepository Rooms { get; }
        IServiceRepository Services { get; }
        IBookingRepository Bookings { get; }

        Task<int> SaveChangesAsync();
    }
}
