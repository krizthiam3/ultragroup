using Bookings.Common;
using Domain.Bookings;
using ErrorOr;
using MediatR;

namespace Application.Bookings.GetById;


internal sealed class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, ErrorOr<BookingResponse>>
{
    private readonly IBookingRepository _bookingRepository;

    public GetBookingByIdQueryHandler(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
    }

    public async Task<ErrorOr<BookingResponse>> Handle(GetBookingByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _bookingRepository.GetByIdAsync(new BookingId(query.Id)) is not Booking booking)
        {
            return Error.NotFound("Booking.NotFound", "The booking with the provide Id was not found.");
        }

        return new BookingResponse(
        booking.Id.Value,
        booking.Code,
        booking.CheckInDate,
        booking.CheckOutDate,
        booking.RoomId,
        booking.CustomerId,
        booking.EmergencyContactFullName,
        booking.EmergencyContactPhoneNumber,
        booking.Active);

 
    }
}