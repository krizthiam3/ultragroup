using FluentValidation;

namespace Application.Rooms.Update;

public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
        RuleFor(r => r.Name).NotEmpty().MaximumLength(100);
        RuleFor(r => r.TypeId).NotEmpty().MaximumLength(2);
        RuleFor(r => r.HotelId).NotEmpty().MaximumLength(2);
        RuleFor(r => r.Occupancy).NotEmpty();
        RuleFor(r => r.UbicationFloor).NotEmpty();
        RuleFor(r => r.Price).NotEmpty();
        RuleFor(r => r.Taxes).NotEmpty();
    }
}