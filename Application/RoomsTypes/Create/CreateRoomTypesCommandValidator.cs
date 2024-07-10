using FluentValidation;

namespace Application.RoomsTypes.Create;

public class CreateRoomTypesCommandValidator : AbstractValidator<CreateRoomTypesCommand>
{
    public CreateRoomTypesCommandValidator()
    {
        RuleFor(r => r.Code).NotEmpty().MaximumLength(20);
        RuleFor(r => r.Name).NotEmpty().MaximumLength(100);      

    }
}