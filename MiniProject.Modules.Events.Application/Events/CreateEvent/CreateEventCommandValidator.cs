using FluentValidation;

namespace MiniProject.Modules.Events.Application.Events.CreateEvent;
internal sealed class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(command => command.Title).NotEmpty().MaximumLength(100);
        RuleFor(command => command.Description).NotEmpty().MaximumLength(500);
        RuleFor(command => command.Location).NotEmpty().MaximumLength(200);
    }
}
