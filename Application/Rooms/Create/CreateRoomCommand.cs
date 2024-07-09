using ErrorOr;
using MediatR;

namespace Application.Rooms.Create;

public record CreateRoomCommand(
    string Code,
    string Name,
    string TypeId,
    string HotelId,
    int Occupancy,
    int UbicationFloor,
    double Price,
    double Taxes
    ) : IRequest<ErrorOr<Guid>>;