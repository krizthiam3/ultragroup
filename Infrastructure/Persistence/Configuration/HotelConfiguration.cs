using Domain.Hotels;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.ToTable("Hotels");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(customerId => customerId.Value, value => new HotelId(value));
        builder.Property(c => c.Name).HasMaxLength(100);
        builder.Property(c => c.Description).HasMaxLength(200);
        builder.Property(c => c.PhoneNumber).HasMaxLength(10);

        builder.OwnsOne(c => c.Address, addressBuilder => {
            addressBuilder.Property(a => a.Country).HasMaxLength(3);
            addressBuilder.Property(a => a.City).HasMaxLength(100);
            addressBuilder.Property(a => a.State).HasMaxLength(100);
            addressBuilder.Property(a => a.ZipCode).HasMaxLength(100).IsRequired(false);
        });

        builder.Property(c => c.Active);
    }
}