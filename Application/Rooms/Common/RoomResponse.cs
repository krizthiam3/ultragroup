namespace Rooms.Common;

public record RoomResponse(
Guid Id,
string Code,
string Name,
string TypeId,
string HotelId,
int Occupancy,
int UbicationFloor,
double Price,
double Taxes,
bool Active);

