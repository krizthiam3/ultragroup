namespace Domain.Bookings;

public interface IBookingRepository
{
    Task<List<Booking>> GetAll();
    Task<Booking?> GetByIdAsync(BookingId id);
    Task<bool> ExistsAsync(BookingId id);
    void Add(Booking booking);
    void Update(Booking booking);
    void Delete(Booking booking);
}