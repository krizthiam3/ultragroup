using Domain.Hotels;

namespace Rooms.Common;

public record RoomResponse(
Guid Id,
string Code,
string Name,
Guid TypeId,
Guid HotelId,
int Occupancy,
int UbicationFloor,
decimal Price,
decimal Taxes,
bool Active);

