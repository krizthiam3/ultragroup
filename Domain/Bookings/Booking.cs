using Domain.Customers;
using Domain.Primitives;
using Domain.Rooms;
using Domain.ValueObjects;
using System.Globalization;

namespace Domain.Bookings;

public sealed class Booking : AggregateRoot
{
    public Booking(BookingId id, string code, DateTime checkInDate, DateTime checkOutDate, Guid roomId, string emergencyNumber, string emergencyName, Guid customerId, bool active)
    {
        Id = id;
        Code = code;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        RoomId = roomId;
        EmergencyContactFullName = emergencyName;
        EmergencyContactPhoneNumber = emergencyNumber;
        CustomerId = customerId;
        Active = active;
    }

    private Booking(){}

    public BookingId Id { get; private set; }
    public string Code { get; private set; } 
    public DateTime CheckInDate { get; private set; } 
    public DateTime CheckOutDate { get; private set; } 
    public Guid RoomId { get; private set; } 
    public string EmergencyContactFullName { get; private set; } 
    public string EmergencyContactPhoneNumber { get; private set; } 
    public Guid CustomerId { get; set; } 
    public bool Active { get; private set; }

    public static Booking UpdateBooking(Guid id, string code, DateTime checkInDate, DateTime checkOutDate, Guid roomId, string emergencyNumber, string emergencyName, Guid customerId, bool active)
    {
        return new Booking(new BookingId(id), code, checkInDate, checkOutDate, roomId, emergencyNumber, emergencyName, customerId, active);
    }
}