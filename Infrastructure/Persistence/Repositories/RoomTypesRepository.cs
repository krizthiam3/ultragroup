using Domain.RoomsTypes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class RoomTypesRepository : IRoomTypesRepository
{
    private readonly ApplicationDbContext _context;

    public RoomTypesRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(RoomTypes roomTypes) => _context.RoomTypes.Add(roomTypes);
    public void Delete(RoomTypes roomTypes) => _context.RoomTypes.Remove(roomTypes);
    public void Update(RoomTypes roomTypes) => _context.RoomTypes.Update(roomTypes);
    public async Task<bool> ExistsAsync(RoomTypesId id) => await _context.RoomTypes.AnyAsync(r => r.Id == id);
    public async Task<RoomTypes?> GetByIdAsync(RoomTypesId id) => await _context.RoomTypes.SingleOrDefaultAsync(c => c.Id == id);
    public async Task<List<RoomTypes>> GetAll() => await _context.RoomTypes.ToListAsync();
}