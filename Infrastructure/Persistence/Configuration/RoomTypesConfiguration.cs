using Domain.RoomsTypes;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class RoomTypesConfiguration : IEntityTypeConfiguration<RoomTypes>
{
    public void Configure(EntityTypeBuilder<RoomTypes> builder)
    {
        builder.ToTable("RoomTypes");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(roomId => roomId.Value, value => new RoomTypesId(value));
        builder.Property(c => c.Code).HasMaxLength(20);
        builder.Property(c => c.Name).HasMaxLength(100);       
        builder.Property(c => c.Active);
    }
}