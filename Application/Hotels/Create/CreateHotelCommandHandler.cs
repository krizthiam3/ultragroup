using Domain.Hotels;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.DomainErrors;
using MediatR;
using ErrorOr;


namespace Application.Hotels.Create;

public sealed class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, ErrorOr<Guid>>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateHotelCommandHandler(IHotelRepository hotelRepository, IUnitOfWork unitOfWork)
    {
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Guid>> Handle(CreateHotelCommand command, CancellationToken cancellationToken)
    {

        if (Address.Create(command.Country, command.City,command.State, command.ZipCode) is not Address address)
        {
            return Errors.Hotel.AddressWithBadFormat;
        }

        var hotel = new Hotel(
            new HotelId(Guid.NewGuid()),
            command.Code,
            command.Name,
            command.Description,
            command.PhoneNumber,
            address,
            true
        );

        _hotelRepository.Add(hotel);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return hotel.Id.Value;
    }
}