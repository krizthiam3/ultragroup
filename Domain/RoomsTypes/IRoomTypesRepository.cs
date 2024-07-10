namespace Domain.RoomsTypes;

public interface IRoomTypesRepository
{
    Task<List<RoomTypes>> GetAll();
    Task<RoomTypes?> GetByIdAsync(RoomTypesId id);
    Task<bool> ExistsAsync(RoomTypesId id);
    void Add(RoomTypes roomTypes);
    void Update(RoomTypes roomTypes);
    void Delete(RoomTypes roomTypes);
}