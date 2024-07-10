using Domain.Customers;
using Domain.Hotels;
using Domain.Rooms;
using Domain.RoomsTypes;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }
    DbSet<Hotel> Hotels { get; set; }
    DbSet<Room> Rooms { get; set; }
    DbSet<RoomTypes> RoomTypes { get; set; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}