using Bookings.Common;
using ErrorOr;
using MediatR;

namespace Application.Bookings.GetById;

public record GetBookingByIdQuery(Guid Id) : IRequest<ErrorOr<BookingResponse>>;