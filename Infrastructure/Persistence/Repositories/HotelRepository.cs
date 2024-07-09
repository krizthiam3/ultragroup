using Domain.Hotels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly ApplicationDbContext _context;

    public HotelRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Hotel hotel) => _context.Hotels.Add(hotel);
    public void Delete(Hotel hotel) => _context.Hotels.Remove(hotel);
    public void Update(Hotel hotel) => _context.Hotels.Update(hotel);
    public async Task<bool> ExistsAsync(HotelId id) => await _context.Hotels.AnyAsync(hotel => hotel.Id == id);
    public async Task<Hotel?> GetByIdAsync(HotelId id) => await _context.Hotels.SingleOrDefaultAsync(c => c.Id == id);
    public async Task<List<Hotel>> GetAll() => await _context.Hotels.ToListAsync();
}