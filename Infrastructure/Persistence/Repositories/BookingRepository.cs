using Domain.Bookings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _context;

    public BookingRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Booking booking) => _context.Booking.Add(booking);
    public void Delete(Booking booking) => _context.Booking.Remove(booking);
    public void Update(Booking booking) => _context.Booking.Update(booking);
    public async Task<bool> ExistsAsync(BookingId id) => await _context.Booking.AnyAsync(booking => booking.Id == id);
    public async Task<Booking?> GetByIdAsync(BookingId id) => await _context.Booking.SingleOrDefaultAsync(c => c.Id == id);
    public async Task<List<Booking>> GetAll() => await _context.Booking.ToListAsync();
}