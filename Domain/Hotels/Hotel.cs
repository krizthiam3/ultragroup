using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Hotels;

public sealed class Hotel : AggregateRoot
{
    public Hotel(HotelId id, string code, string name, string description, string phoneNumber, Address address, bool active)
    {
        Id = id;
        Code = code;
        Name = name;
        Description = description;
        PhoneNumber = phoneNumber;
        Address = address;
        Active = active;
    }

    private Hotel()
    {

    }

    public HotelId Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PhoneNumber { get; private set; }
    public Address Address { get; private set; }
    public bool Active { get; private set; }

    public static Hotel UpdateHotel(Guid id, string name, string code, string description,  string phoneNumber, Address address, bool active)
    {
        return new Hotel(new HotelId(id), name, code, description, phoneNumber, address, active);
    }
}