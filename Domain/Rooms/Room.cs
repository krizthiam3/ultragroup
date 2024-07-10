using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Rooms;

public sealed class Room : AggregateRoot
{
    public Room(RoomId id, string code,string name, string type, string hotel, int occupancy, int floor, double price, double taxes, bool active)
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
    public string TypeId { get; private set; } 
    public string HotelId { get; private set; } 
    public int Occupancy { get; set; } 
    public int UbicationFloor { get; private set; } 
    public double Price { get; private set; }
    public double Taxes { get; private set; }
    public bool Active { get; private set; }

    public static Room UpdateRoom(Guid id, string code, string name, string type, string hotel, int occupancy, int floor, double price, double taxes, bool active)
    {
        return new Room(new RoomId(id), code, name, type, hotel, occupancy, floor, price, taxes, active);
    }
}

