using Application.Bookings.Create;
using Application.Bookings.Update;
using Application.Bookings.GetById;
using Application.Bookings.Delete;
using Application.Bookings.GetAll;

namespace Web.API.Controllers;

[Route("bookings")]
public class Bookings : ApiController
{
    private readonly ISender _mediator;

    public Bookings(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bookingsResult = await _mediator.Send(new GetAllBookingsQuery());

        return bookingsResult.Match(
            bookings => Ok(bookings),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var bookingResult = await _mediator.Send(new GetBookingByIdQuery(id));

        return bookingResult.Match(
            booking => Ok(booking),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingCommand command)
    {
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            bookingId => Ok(bookingId),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookingCommand command)
    {
        if (command.Id != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Booking.UpdateInvalid", "The request Id does not match with the url Id.")
            };
            return Problem(errors);
        }

        var updateResult = await _mediator.Send(command);

        return updateResult.Match(
            bookingId => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteBookingCommand(id));

        return deleteResult.Match(
            bookingId => NoContent(),
            errors => Problem(errors)
        );
    }
}