using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Rooms;

public sealed class Room : AggregateRoot
{
    public Room(RoomId id, string code,string name, Guid type, Guid hotel, int occupancy, int floor, decimal price, decimal taxes, bool active)
    {
        Id = id;
        Code = code;
        Name = name;
        TypeId = type;
        HotelId = hotel;
        Occupancy = occupancy;
        UbicationFloor = floor;
        Price = price;
        Taxes = taxes;
        Active = active;
    }

    private Room()
    {

    }

    public RoomId Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public Guid TypeId { get; private set; } 
    public Guid HotelId { get; private set; } 
    public int Occupancy { get; set; } 
    public int UbicationFloor { get; private set; } 
    public decimal Price { get; private set; }
    public decimal Taxes { get; private set; }
    public bool Active { get; private set; }

    public static Room Update(Guid id, string code, string name, Guid type, Guid hotel, int occupancy, int floor, decimal price, decimal taxes, bool active)
    {
        return new Room(new RoomId(id), code, name, type, hotel, occupancy, floor, price, taxes, active);
    }

}

