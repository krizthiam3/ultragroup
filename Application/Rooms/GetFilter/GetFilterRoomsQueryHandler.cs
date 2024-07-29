using Rooms.Common;
using Domain.Rooms;
using Domain.Hotels;
using ErrorOr;
using MediatR;
using Domain.Bookings;
namespace Application.Rooms.GetFilter;


internal sealed class GetFilterRoomsQueryHandler : IRequestHandler<GetFilterRoomsQuery, ErrorOr<IReadOnlyList<RoomResponse>>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IBookingRepository _bookingRepository;

    public GetFilterRoomsQueryHandler(IRoomRepository roomRepository, IHotelRepository hotelRepository, IBookingRepository bookingRepository)
    {
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<RoomResponse>>> Handle(GetFilterRoomsQuery query, CancellationToken cancellationToken)
    {
        /// Validatios
        if (query.StartDate <= DateTime.MinValue.Date && query.EndDate.Date <= DateTime.MinValue.Date)
        {
            return Error.NotFound("Room Rooms.Filters", $"The request have incorrect format for date parameters  ");
        }

        if (query.StartDate.Date < DateTime.Now.Date || query.EndDate.Date < DateTime.Now.Date)
        {
            return Error.NotFound("Room Rooms.Filters", $"The request have incorrect date parameters, the start date y endDate must be greater than or equal to the current date ");
        }

        if (query.StartDate.Date > query.EndDate.Date)
        {
            return Error.NotFound("Room Rooms.Filters", $"The request have incorrect date parameters, The endDate must not be greater than the startDate ");
        }

        IReadOnlyList<Room> rooms = await _roomRepository.GetAll();
        IReadOnlyList<Hotel> hotels = await _hotelRepository.GetAll();
        IReadOnlyList<Booking> bookings  = await _bookingRepository.GetAll();

        if (query.Occupancy > 0)        
            rooms = rooms.Where(x => x.Active == query.Status && x.Occupancy == query.Occupancy).ToList();        

        if (!string.IsNullOrEmpty(query.City))        
            hotels = hotels.Where(x => x.Address.City == query.City).ToList();  

        
         DateTime startDateConvertida;
         DateTime endDateConvertida;

         DateTime.TryParse(query.StartDate.ToString(), out startDateConvertida);
         DateTime.TryParse(query.EndDate.ToString(), out endDateConvertida);

         bookings = bookings.Where(x => x.CheckInDate >= query.StartDate && x.CheckOutDate <= query.EndDate).ToList();

         if (bookings.Count > 0)
         {
             var filteredBookingsIds = bookings.Select(b => b.RoomId).ToHashSet();
             rooms = rooms.Where(room => !filteredBookingsIds.Contains(room.Id.Value)).ToList();
         }        

        var filteredHotelIds = hotels.Select(h => h.Id.Value).ToHashSet();
        var filteredRooms = rooms.Where(room => filteredHotelIds.Contains(room.HotelId) &&
                                                room.Active == query.Status &&
                                                room.Occupancy == query.Occupancy).ToList();
        

        return filteredRooms.Select(room => new RoomResponse(
                room.Id.Value,
                room.Code,
                room.Name,
                room.TypeId,
                room.HotelId,
                room.Occupancy,
                room.UbicationFloor,
                room.Price,
                room.Taxes,
                room.Active
            )).ToList();
    }
}
