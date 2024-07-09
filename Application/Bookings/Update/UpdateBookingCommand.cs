namespace Application.Bookings.Update;
using ErrorOr;
using MediatR;

public record UpdateBookingCommand(
    Guid Id,
    string Code,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    string RoomId,
    string CustomerId,
     string EmergencyContactFullName,
    string EmergencyContactPhoneNumber,
    bool Active) : IRequest<ErrorOr<Unit>>;

