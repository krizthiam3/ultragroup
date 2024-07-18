using Domain.Bookings;
using Domain.Rooms;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.DomainErrors;
using MediatR;
using ErrorOr;
using Domain.Customers;


namespace Application.Bookings.Create;

public sealed class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, ErrorOr<Guid>>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateBookingCommandHandler(IBookingRepository bookingRepository, IRoomRepository roomRepository, ICustomerRepository customerRepository,  IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Guid>> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
    {
        /// Validatios
        /// 
        if (!await _customerRepository.ExistsAsync(new CustomerId(command.CustomerId)))
        {
            return Error.NotFound("CustomerId.NotFound", $"The Customer with the provide Id {command.CustomerId} was not found.");
        }


        if (!await _roomRepository.ExistsAsync(new RoomId(command.RoomId)))
        {
            return Error.NotFound("RoomId.NotFound", $"The Room with the provide Id {command.RoomId} was not found.");
        }

        var booking = new Booking(
            new BookingId(Guid.NewGuid()),
            command.Code,
            command.CheckInDate,
            command.CheckOutDate,
            command.RoomId,
            command.EmergencyContactFullName,
            command.EmergencyContactPhoneNumber,
            command.CustomerId,
            true
        );

        _bookingRepository.Add(booking);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return booking.Id.Value;
    }
}