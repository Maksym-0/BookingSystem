using Microsoft.EntityFrameworkCore;
using Booking.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Postgres.Configurations
{
    internal class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.BasePricePerHour)
                .HasPrecision(18, 2);

            builder.HasMany(r => r.AvailableServices)
                .WithMany(s => s.Rooms)
                .UsingEntity(j => j.ToTable("RoomServices"));
        }
    }
}
