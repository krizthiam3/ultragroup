using Domain.Bookings;
using ErrorOr;
using MediatR;
using Bookings.Common;

namespace Application.Bookings.GetAll;


internal sealed class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, ErrorOr<IReadOnlyList<BookingResponse>>>
{
    private readonly IBookingRepository _roomRepository;

    public GetAllBookingsQueryHandler(IBookingRepository roomRepository)
    {
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<BookingResponse>>> Handle(GetAllBookingsQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<Booking> bookings = await _roomRepository.GetAll();

        return bookings.Select(booking => new BookingResponse(
                booking.Id.Value,
                booking.Code,
                booking.CheckInDate,
                booking.CheckOutDate,
                booking.RoomId,
                booking.CustomerId,
                booking.EmergencyContactFullName,
                booking.EmergencyContactPhoneNumber,
                booking.Active
            )).ToList();
    }
}
