namespace Application.Rooms.Update;
using ErrorOr;
using MediatR;

public record UpdateRoomCommand(
    Guid Id,
    string Code,
    string Name,
    string TypeId,
    string HotelId,
    int Occupancy,
    int UbicationFloor,
    double Price,
    double Taxes,
    bool Active) : IRequest<ErrorOr<Unit>>;

