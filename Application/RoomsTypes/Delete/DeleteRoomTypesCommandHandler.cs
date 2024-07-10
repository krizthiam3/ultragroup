using Domain.RoomsTypes;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using Application.Rooms.Delete;

namespace Application.RoomsTypes.Delete;

internal sealed class DeleteRoomTypesCommandHandler : IRequestHandler<DeleteRoomTypesCommand, ErrorOr<Unit>>
{
    private readonly IRoomTypesRepository _roomTypesRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteRoomTypesCommandHandler(IRoomTypesRepository roomTypesRepository, IUnitOfWork unitOfWork)
    {
        _roomTypesRepository = roomTypesRepository ?? throw new ArgumentNullException(nameof(roomTypesRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteRoomTypesCommand command, CancellationToken cancellationToken)
    {
        if (await _roomTypesRepository.GetByIdAsync(new RoomTypesId(command.Id)) is not RoomTypes roomTypes)
        {
            return Error.NotFound("RoomTypes.NotFound", "The roomTypes with the provide Id was not found.");
        }

        _roomTypesRepository.Delete(roomTypes);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
