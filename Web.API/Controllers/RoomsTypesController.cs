using Application.RoomsTypes.Create;
using Application.RoomsTypes.Update;
using Application.RoomsTypes.GetById;
using Application.RoomsTypes.Delete;
using Application.RoomsTypes.GetAll;
using Domain.RoomsTypes;

namespace Web.API.Controllers;

[Route("roomsTypes")]
public class RoomsTypes : ApiController
{
    private readonly ISender _mediator;

    public RoomsTypes(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roomsResult = await _mediator.Send(new GetAllRoomsTypesQuery());

        return roomsResult.Match(
            rooms => Ok(rooms),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var roomsResult = await _mediator.Send(new GetRoomTypesByIdQuery(id));

        return roomsResult.Match(
            rooms => Ok(rooms),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoomTypesCommand command)
    {
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            roomId => Ok(roomId),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoomTypesCommand command)
    {
        if (command.Id != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("RoomTypes.UpdateInvalid", "The request Id does not match with the url Id.")
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
        var deleteResult = await _mediator.Send(new DeleteRoomTypesCommand(id));

        return deleteResult.Match(
            roomId => NoContent(),
            errors => Problem(errors)
        );
    }
}