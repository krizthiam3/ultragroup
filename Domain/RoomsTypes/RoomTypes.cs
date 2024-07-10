using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.RoomsTypes;

public sealed class RoomTypes : AggregateRoot
{
    public RoomTypes(RoomTypesId id, string code,string name, bool active)
    {
        Id = id;
        Code = code;
        Name = name;
        Active = active;
    }

    private RoomTypes(){}

    public RoomTypesId Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public bool Active { get; private set; }

    public static RoomTypes UpdateTypes(Guid id, string code, string name, bool active)
    {
        return new RoomTypes(new RoomTypesId(id), code, name, active);
    }
}

