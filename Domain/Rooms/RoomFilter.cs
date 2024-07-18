using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Rooms;

public sealed class RoomFilter
{
    public int Occupancy { get; set; }   
    public string City{ get; set; }

    //public bool Active { get; set; }

}

