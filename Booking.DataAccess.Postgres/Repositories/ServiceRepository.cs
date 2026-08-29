using Booking.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

using Booking.Core.Models;

namespace Booking.DataAccess.Postgres.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly BookingDbContext _context;

        public ServiceRepository(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task<List<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<List<Service>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Services
                .Where(s => ids.Contains(s.Id))
                .ToListAsync();
        }

        public void Add(Service service) => _context.Services.Add(service);
        public void Delete(Service service) => _context.Services.Remove(service);
    }
}
