using Rooms.Common;
using Domain.Rooms;
using Domain.Hotels;
using ErrorOr;
using MediatR;

namespace Application.Rooms.GetFilter;


internal sealed class GetFilterRoomsQueryHandler : IRequestHandler<GetFilterRoomsQuery, ErrorOr<IReadOnlyList<RoomResponse>>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IHotelRepository _hotelRepository;

    public GetFilterRoomsQueryHandler(IRoomRepository roomRepository, IHotelRepository hotelRepository)
    {
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<RoomResponse>>> Handle(GetFilterRoomsQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<Room> rooms = await _roomRepository.GetAll();
        IReadOnlyList<Hotel> hotels = await _hotelRepository.GetAll();


        if (query.Occupancy > 0)
        {
            rooms = rooms.Where(x => x.Active == query.Status && x.Occupancy == query.Occupancy).ToList();
        }

        if (!string.IsNullOrEmpty(query.City))
        {
            hotels = hotels.Where(x => x.Address.City == query.City).ToList();
        }

        var filteredHotelIds = hotels.Select(h => h.Id.Value).ToHashSet();

        // Filtrar habitaciones por los hoteles filtrados y otros criterios
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
