using Domain.Bookings;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Booking");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(roomId => roomId.Value, value => new BookingId(value));
        builder.Property(c => c.Code).HasMaxLength(20);
        builder.Property(c => c.CheckInDate).IsRequired();
        builder.Property(c => c.CheckOutDate).IsRequired();
        builder.Property(c => c.RoomId).HasMaxLength(50);
        builder.Property(c => c.EmergencyContactPhoneNumber).HasMaxLength(200);
        builder.Property(c => c.EmergencyContactFullName).HasMaxLength(100);
        builder.Property(c => c.CustomerId).HasMaxLength(50);
        builder.Property(c => c.Active);
    }
}