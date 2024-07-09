using Hotels.Common;
using Domain.Hotels;
using ErrorOr;
using MediatR;

namespace Application.Hotels.GetAll;


internal sealed class GetAllHotelsQueryHandler : IRequestHandler<GetAllHotelsQuery, ErrorOr<IReadOnlyList<HotelResponse>>>
{
    private readonly IHotelRepository _hotelRepository;

    public GetAllHotelsQueryHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<HotelResponse>>> Handle(GetAllHotelsQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<Hotel> hotels = await _hotelRepository.GetAll();

        return hotels.Select(hotel => new HotelResponse(
                hotel.Id.Value,
                hotel.Code,
                hotel.Name,
                hotel.Description,
                hotel.PhoneNumber,
                new AddressResponse(hotel.Address.Country,
                    hotel.Address.City,
                    hotel.Address.State,
                    hotel.Address.ZipCode),
                    hotel.Active
            )).ToList();
    }
}