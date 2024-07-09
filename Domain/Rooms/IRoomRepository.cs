namespace Domain.Rooms;

public interface IRoomRepository
{
    Task<List<Room>> GetAll();
    Task<Room?> GetByIdAsync(RoomId id);
    Task<bool> ExistsAsync(RoomId id);
    void Add(Room room);
    void Update(Room room);
    void Delete(Room room);
}