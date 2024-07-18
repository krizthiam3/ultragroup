using Application.Hotels.Create;
using Application.Hotels.Update;
using Application.Hotels.GetById;
using Application.Hotels.Delete;
using Application.Hotels.GetAll;
using Application.Rooms.Update;

namespace Web.API.Controllers;

[Route("hotels")]
public class Hotels : ApiController
{
    private readonly ISender _mediator;

    public Hotels(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var hotelsResult = await _mediator.Send(new GetAllHotelsQuery());
        return hotelsResult.Match(hotel => Ok(hotel), errors => Problem(errors));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var hotelsResult = await _mediator.Send(new GetHotelByIdQuery(id));
        return hotelsResult.Match(hotel => Ok(hotel), errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHotelCommand command)
    {
        var createResult = await _mediator.Send(command);
        return createResult.Match(hotelId => Ok(hotelId), errors => Problem(errors));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateHotelCommand command)
    {
        if (command.Id != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Hotel.UpdateInvalid", "The request Id does not match with the url Id.")
            };

            return Problem(errors);
        }

        var updateResult = await _mediator.Send(command);
        return updateResult.Match(hotelId => NoContent(), errors => Problem(errors));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteHotelCommand(id));
        return deleteResult.Match(hotelId => NoContent(), errors => Problem(errors));
    }


    [HttpPatch("{id}/status")]
    public async Task<IActionResult> StatusChange(Guid id, [FromBody] UpdateHotelCommand command)
    {
        try
        {
            List<Error> errors = new();

            if (command.Id != id)
            {
                errors.Add(Error.Validation("Hotel.ChangeStatus", "The request Id does not match with the url Id."));
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