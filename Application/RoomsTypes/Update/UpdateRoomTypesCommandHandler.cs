using Domain.RoomsTypes;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.RoomsTypes.Update;

internal sealed class UpdateRoomTypesCommandHandler : IRequestHandler<UpdateRoomTypesCommand, ErrorOr<Unit>>
{
    private readonly IRoomTypesRepository _roomTypesRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateRoomTypesCommandHandler(IRoomTypesRepository roomTypesRepository, IUnitOfWork unitOfWork)
    {
        _roomTypesRepository = roomTypesRepository ?? throw new ArgumentNullException(nameof(roomTypesRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(UpdateRoomTypesCommand command, CancellationToken cancellationToken)
    {
        if (!await _roomTypesRepository.ExistsAsync(new RoomTypesId(command.Id)))
        {
            return Error.NotFound("RoomTypes.NotFound", "The roomTypes with the provide Id was not found.");
        }

        RoomTypes roomTypes = RoomTypes.UpdateTypes(command.Id, command.Code, command.Name, command.Active);

        _roomTypesRepository.Update(roomTypes);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
