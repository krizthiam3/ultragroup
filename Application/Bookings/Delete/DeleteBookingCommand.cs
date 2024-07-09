namespace Application.Bookings.Delete;
using ErrorOr;
using MediatR;

public record DeleteBookingCommand(Guid Id) : IRequest<ErrorOr<Unit>>;