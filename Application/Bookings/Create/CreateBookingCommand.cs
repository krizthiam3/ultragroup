using ErrorOr;
using MediatR;

namespace Application.Bookings.Create;

public record CreateBookingCommand(
    string Code,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    Guid RoomId,
    Guid CustomerId,
    string EmergencyContactFullName,
    string EmergencyContactPhoneNumber,
    bool Active
) : IRequest<ErrorOr<Guid>>;