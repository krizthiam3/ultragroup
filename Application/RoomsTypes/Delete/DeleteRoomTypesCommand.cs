namespace Application.RoomsTypes.Delete;
using ErrorOr;
using MediatR;

public record DeleteRoomTypesCommand(Guid Id) : IRequest<ErrorOr<Unit>>;