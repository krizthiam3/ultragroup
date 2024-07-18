using Domain.Bookings;
using Domain.Customers;
using Domain.Primitives;
using Domain.Rooms;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Bookings.Update;

internal sealed class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, ErrorOr<Unit>>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateBookingCommandHandler(IBookingRepository bookingRepository, IRoomRepository roomRepository, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(UpdateBookingCommand command, CancellationToken cancellationToken)
    {
        if (!await _bookingRepository.ExistsAsync(new BookingId(command.Id)))
        {
            return Error.NotFound("Booking.NotFound", "The booking with the provide Id was not found.");
        }

        if (!await _customerRepository.ExistsAsync(new CustomerId(command.CustomerId)))
        {
            return Error.NotFound("CustomerId.NotFound", $"The Customer with the provide Id {command.CustomerId} was not found.");
        }


        if (!await _roomRepository.ExistsAsync(new RoomId(command.RoomId)))
        {
            return Error.NotFound("RoomId.NotFound", $"The Room with the provide Id {command.RoomId} was not found.");
        }

        Booking booking = Booking.UpdateBooking(command.Id,
            command.Code,
            command.CheckInDate,
            command.CheckOutDate,
            command.RoomId,
            command.EmergencyContactFullName,
            command.EmergencyContactPhoneNumber,
            command.CustomerId,
            command.Active); 

        _bookingRepository.Update(booking);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
