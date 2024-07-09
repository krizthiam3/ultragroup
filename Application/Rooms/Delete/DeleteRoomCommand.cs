namespace Application.Rooms.Delete;
using ErrorOr;
using MediatR;

public record DeleteRoomCommand(Guid Id) : IRequest<ErrorOr<Unit>>;