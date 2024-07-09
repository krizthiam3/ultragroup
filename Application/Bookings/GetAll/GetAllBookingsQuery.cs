using ErrorOr;
using MediatR;
using Bookings.Common;

namespace Application.Bookings.GetAll;

public record GetAllBookingsQuery() : IRequest<ErrorOr<IReadOnlyList<BookingResponse>>>;