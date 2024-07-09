using static Domain.DomainErrors.Errors;

namespace Domain.Hotels;

public interface IHotelRepository
{
    Task<List<Hotel>> GetAll();
    Task<Hotel?> GetByIdAsync(HotelId id);
    Task<bool> ExistsAsync(HotelId id);
    void Add(Hotel hotel);
    void Update(Hotel hotel);
    void Delete(Hotel hotel);
}