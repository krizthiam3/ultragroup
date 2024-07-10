using FluentValidation;

namespace Application.RoomsTypes.Delete;

public class DeleteRoomTypesCommandValidator : AbstractValidator<DeleteRoomTypesCommand>
{
    public DeleteRoomTypesCommandValidator()
    {
        RuleFor(r => r.Id).NotEmpty();
    }
}