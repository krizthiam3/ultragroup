using Application.Rooms.Create;
using Application.Rooms.Update;
using Application.Rooms.GetById;
using Application.Rooms.Delete;
using Application.Rooms.GetAll;
using Domain.Rooms;

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
}