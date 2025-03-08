using FluentValidation;

namespace MiniProject.Modules.Events.Application.Events.GetEventTest;
internal sealed class GetEventTestQueryValidator : AbstractValidator<GetEventTestQuery>
{
    public GetEventTestQueryValidator()
    {
        RuleFor(x => x.EventId).Matches("^[0-9 ]+$");
    }
}
