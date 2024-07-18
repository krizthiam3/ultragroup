using Application.Rooms.Create;
using Application.Rooms.Update;
using Application.Rooms.GetById;
using Application.Rooms.Delete;
using Application.Rooms.GetAll;
using Domain.Rooms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Rooms.GetFilter;

namespace Web.API.Controllers;

[Route("rooms")]
public class Rooms : ApiController
{
    private readonly ISender _mediator;

    public Rooms(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roomsResult = await _mediator.Send(new GetAllRoomsQuery());

        return roomsResult.Match(
            rooms => Ok(rooms),
            errors => Problem(errors)
        );
    }

    [HttpGet("/filters")]
    public async Task<IActionResult> GetRooms([FromQuery] RoomFilter filter)
    {
        var roomsResult = await _mediator.Send(new GetFilterRoomsQuery(filter.City, filter.Occupancy, true));

        return roomsResult.Match(
            rooms => Ok(rooms),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var roomsResult = await _mediator.Send(new GetRoomByIdQuery(id));

        return roomsResult.Match(
            rooms => Ok(rooms),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoomCommand command)
    {
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            roomId => Ok(roomId),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoomCommand command)
    {
        if (command.Id != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Room.UpdateInvalid", "The request Id does not match with the url Id.")
            };
            return Problem(errors);
        }

        var updateResult = await _mediator.Send(command);

        return updateResult.Match(
            roomId => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteRoomCommand(id));

        return deleteResult.Match(
            roomId => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> StatusChange(Guid id, [FromBody] UpdateRoomCommand command)
    {
        try
        {
            List<Error> errors = new();

            if (command.Id != id)
            {
                errors.Add(Error.Validation("Room.ChangeStatus", "The request Id does not match with the url Id."));
                return Problem(errors);
            }

             var updateResult = await _mediator.Send(command);

             return updateResult.Match(
                    roomId => NoContent(),
                    errors => Problem(errors)
             );
            
        }
        catch (Exception ex)
        {
            return Problem("An unexpected error occurred.");
        }

    }
}