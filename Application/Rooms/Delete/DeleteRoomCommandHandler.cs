using Domain.Rooms;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Rooms.Delete;

internal sealed class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, ErrorOr<Unit>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteRoomCommandHandler(IRoomRepository  roomRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        if (await _roomRepository.GetByIdAsync(new RoomId(command.Id)) is not Room room)
        {
            return Error.NotFound("Room.NotFound", "The hotel with the provide Id was not found.");
        }

        _roomRepository.Delete(room);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
