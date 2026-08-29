using Booking.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Postgres.Configurations
{
    internal class BookingRecordConfiguration : IEntityTypeConfiguration<BookingRecord>
    {
        public void Configure(EntityTypeBuilder<BookingRecord> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.TotalPrice)
                .HasPrecision(18, 2);

            builder.HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.SelectedServices)
                .WithMany(s => s.Bookings)
                .UsingEntity(j => j.ToTable("BookingSelectedServices"));
        }
    }
}
