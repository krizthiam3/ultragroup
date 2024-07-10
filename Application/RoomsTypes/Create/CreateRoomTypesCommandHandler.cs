using Domain.RoomsTypes;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.DomainErrors;
using MediatR;
using ErrorOr;


namespace Application.RoomsTypes.Create;

public sealed class CreateRoomTypesCommandHandler : IRequestHandler<CreateRoomTypesCommand, ErrorOr<Guid>>
{
    private readonly IRoomTypesRepository _roomTypesRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateRoomTypesCommandHandler(IRoomTypesRepository roomTypesRepository, IUnitOfWork unitOfWork)
    {
        _roomTypesRepository = roomTypesRepository ?? throw new ArgumentNullException(nameof(roomTypesRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Guid>> Handle(CreateRoomTypesCommand command, CancellationToken cancellationToken)
    {
        /// Validatios


        var room = new RoomTypes(
            new RoomTypesId(Guid.NewGuid()),
            command.Code,
            command.Name,           
            true
        );

        _roomTypesRepository.Add(room);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return room.Id.Value;
    }
}