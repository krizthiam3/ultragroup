namespace Application.Rooms.Update;
using ErrorOr;
using MediatR;

public record UpdateRoomCommand(
    Guid Id,
    string Code,
    string Name,
    Guid TypeId,
    Guid HotelId,
    int Occupancy,
    int UbicationFloor,
    decimal Price,
    decimal Taxes,
    bool Active) : IRequest<ErrorOr<Unit>>;

