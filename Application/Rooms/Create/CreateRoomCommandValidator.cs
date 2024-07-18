using FluentValidation;

namespace Application.Rooms.Create;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(r => r.Name).NotEmpty().MaximumLength(100);

        RuleFor(r => r.TypeId).NotEmpty();

        RuleFor(r => r.HotelId).NotEmpty();

        RuleFor(r => r.Occupancy).NotEmpty();

        RuleFor(r => r.UbicationFloor).NotEmpty();

        RuleFor(r => r.Price).NotEmpty();

        RuleFor(r => r.Taxes).NotEmpty();

    }
}