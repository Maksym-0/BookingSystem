using Booking.Core.Models;

namespace Booking.Core.Interfaces.Repositories
{
    public interface IServiceRepository
    {
        Task<Service?> GetByIdAsync(Guid id);
        Task<List<Service>> GetAllAsync();

        Task<List<Service>> GetByIdsAsync(IEnumerable<Guid> ids);

        void Add(Service service);
        void Delete(Service service);
    }
}
