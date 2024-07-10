using Domain.Rooms;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _context;

    public RoomRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Room room) => _context.Rooms.Add(room);
    public void Delete(Room room) => _context.Rooms.Remove(room);
    public void Update(Room room) => _context.Rooms.Update(room);
    public async Task<bool> ExistsAsync(RoomId id) => await _context.Rooms.AnyAsync(r => r.Id == id);
    public async Task<Room?> GetByIdAsync(RoomId id) => await _context.Rooms.SingleOrDefaultAsync(c => c.Id == id);
    public async Task<List<Room>> GetAll() => await _context.Rooms.ToListAsync();
}