namespace Application.RoomsTypes.Update;
using ErrorOr;
using MediatR;

public record UpdateRoomTypesCommand(
    Guid Id,
    string Code,
    string Name,
    bool Active) : IRequest<ErrorOr<Unit>>;

