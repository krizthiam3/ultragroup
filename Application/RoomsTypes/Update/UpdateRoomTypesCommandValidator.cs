using Application.RoomsTypes.Update;
using FluentValidation;

namespace Application.RoomsTypes.Update;

public class UpdateRoomTypesCommandValidator : AbstractValidator<UpdateRoomTypesCommand>
{
    public UpdateRoomTypesCommandValidator()
    {
        RuleFor(r => r.Code).NotEmpty().MaximumLength(20);
        RuleFor(r => r.Name).NotEmpty().MaximumLength(100);
    }
}