namespace Bookings.Common;

public record BookingResponse(
    Guid Id,
    string Code,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    Guid RoomId,
    Guid CustomerId,
    string EmergencyContactFullName,
    string EmergencyContactPhoneNumber,
    bool Active
);

