using Booking.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Postgres
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<BookingRecord> BookingRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
