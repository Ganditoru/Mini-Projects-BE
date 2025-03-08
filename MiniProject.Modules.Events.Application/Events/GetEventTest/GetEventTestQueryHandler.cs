using MediatR;

namespace MiniProject.Modules.Events.Application.Events.GetEventTest;
internal sealed class GetEventTestQueryHandler() : IRequestHandler<GetEventTestQuery, EventResponse?>
{
    public async Task<EventResponse?> Handle(GetEventTestQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);


        if (request.EventId.Equals(" ", StringComparison.Ordinal))
        {
            return null;
        }

        var response = new EventResponse(Guid.NewGuid(), "Test Event", "This is a test description Event", "On local");

        return response;
    }
}
