using Domain.Rooms;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(roomId => roomId.Value, value => new RoomId(value));
        builder.Property(c => c.Code).HasMaxLength(20);
        builder.Property(c => c.Name).HasMaxLength(100);
        builder.Property(c => c.TypeId).HasMaxLength(20);
        builder.Property(c => c.HotelId).HasMaxLength(20);
        builder.Property(c => c.Occupancy).HasMaxLength(2);
        builder.Property(c => c.UbicationFloor).HasMaxLength(2);
        builder.Property(c => c.Price).HasMaxLength(10);
        builder.Property(c => c.Taxes).HasMaxLength(10);
        builder.Property(c => c.Active);
    }
}