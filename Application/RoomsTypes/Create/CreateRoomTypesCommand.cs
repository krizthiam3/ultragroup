using ErrorOr;
using MediatR;

namespace Application.RoomsTypes.Create;

public record CreateRoomTypesCommand(string Code, string Name) : IRequest<ErrorOr<Guid>>;