namespace Bookings.Common;

public record BookingResponse(
    Guid Id,
    string Code,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    string RoomId,
    string CustomerId,
    string EmergencyContactFullName,
    string EmergencyContactPhoneNumber,
    bool Active
);

