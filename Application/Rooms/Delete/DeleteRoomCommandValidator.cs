using FluentValidation;

namespace Application.Rooms.Delete;

public class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
{
    public DeleteRoomCommandValidator()
    {
        RuleFor(r => r.Id).NotEmpty();
    }
}