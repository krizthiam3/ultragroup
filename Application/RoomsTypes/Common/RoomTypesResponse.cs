namespace RoomsTypes.Common;

public record RoomTypesResponse(
    Guid Id,
    string Code,
    string Name,
    bool Active
);

