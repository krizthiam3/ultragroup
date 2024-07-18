namespace Application.Bookings.Update;
using ErrorOr;
using MediatR;

public record UpdateBookingCommand(
    Guid Id,
    string Code,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    Guid RoomId,
    Guid CustomerId,
    string EmergencyContactFullName,
    string EmergencyContactPhoneNumber,
    bool Active) : IRequest<ErrorOr<Unit>>;

