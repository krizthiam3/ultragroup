using ErrorOr;
using MediatR;

namespace Application.Rooms.Create;

public record CreateRoomCommand(
    string Code,
    string Name,
    Guid TypeId,
    Guid HotelId,
    int Occupancy,
    int UbicationFloor,
    decimal Price,
    decimal Taxes
    ) : IRequest<ErrorOr<Guid>>;