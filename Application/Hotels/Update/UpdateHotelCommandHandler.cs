using Domain.Hotels;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Hotels.Update;

internal sealed class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, ErrorOr<Unit>>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateHotelCommandHandler(IHotelRepository hotelRepository, IUnitOfWork unitOfWork)
    {
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(UpdateHotelCommand command, CancellationToken cancellationToken)
    {
        if (!await _hotelRepository.ExistsAsync(new HotelId(command.Id)))
        {
            return Error.NotFound("Customer.NotFound", "The hotel with the provide Id was not found.");
        }


        if (Address.Create(command.Country, command.City, command.State, command.ZipCode) is not Address address)
        {
            return Error.Validation("Hotel.Address", "Address is not valid.");
        }

        Hotel hotel = Hotel.UpdateHotel(command.Id, command.Code,
            command.Name,
            command.Description,
            command.PhoneNumber,
            address,
            command.Active);

        _hotelRepository.Update(hotel);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
