using Booking.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Postgres
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(BookingDbContext context)
        {
            if (!await context.Services.AnyAsync())
            {
                var services = new List<Service>
                {
                    new Service("Проєктор", 500m),
                    new Service("Wi-Fi", 300m),
                    new Service("Звук", 700m)
                };

                await context.Services.AddRangeAsync(services);
                await context.SaveChangesAsync();
            }

            if (!await context.Rooms.AnyAsync())
            {
                var rooms = new List<Room>
                {
                    new Room("Зал А", 50, 2000m),
                    new Room("Зал B", 100, 3500m),
                    new Room("Зал C", 30, 1500m)
                };

                var allServices = await context.Services.ToListAsync();
                foreach (var room in rooms)
                {
                    foreach (var service in allServices)
                    {
                        room.AddService(service);
                    }
                }

                await context.Rooms.AddRangeAsync(rooms);
                await context.SaveChangesAsync();
            }
        }
    }
}
