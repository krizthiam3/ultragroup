using Domain.Customers;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
       // builder.ToTable("Customers");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(customerId => customerId.Value, value => new CustomerId(value));
        builder.Property(c => c.Name).HasMaxLength(100);
        builder.Property(c => c.LastName).HasMaxLength(100);
        builder.Ignore(c => c.FullName);
        builder.Property(c => c.Email).HasMaxLength(100);
        builder.HasIndex(c => c.Email).IsUnique();
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