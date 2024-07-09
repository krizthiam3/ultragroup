using Hotels.Common;
using Domain.Hotels;
using ErrorOr;
using MediatR;

namespace Application.Hotels.GetById;


internal sealed class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, ErrorOr<HotelResponse>>
{
    private readonly IHotelRepository _hotelRepository;

    public GetHotelByIdQueryHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
    }

    public async Task<ErrorOr<HotelResponse>> Handle(GetHotelByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _hotelRepository.GetByIdAsync(new HotelId(query.Id)) is not Hotel hotel)
        {
            return Error.NotFound("Customer.NotFound", "The hotel with the provide Id was not found.");
        }

        return new HotelResponse(
            hotel.Id.Value, 
            hotel.Code,
            hotel.Name, 
            hotel.Description, 
            hotel.PhoneNumber, 
            new AddressResponse(hotel.Address.Country,
            hotel.Address.City,
            hotel.Address.State,
            hotel.Address.ZipCode),
            hotel.Active);
    }
}