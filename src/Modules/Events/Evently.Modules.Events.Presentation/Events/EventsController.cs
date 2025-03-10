using Evently.Modules.Events.Application.Events.CreateEvent;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Application.Events.GetTest;
using Evently.Modules.Events.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Evently.Modules.Events.Presentation.Events;
public sealed class EventsController(ISender sender): ApiController
{
    [HttpGet("test/{id}")]
    public async Task<string> GetTest(string id)
    {
        string response = await sender.Send(new GetTestQuery(id));

        return response;
    }

    [HttpPost]
    public async Task<Guid> CreateEvent([FromBody] EventRequest request) {
        var command = new CreateEventCommand(
            Guid.NewGuid(),
            request.Title,
            request.Description,
            request.Location
        );

        Guid eventId = await sender.Send(command);

        return eventId;
    }

    [HttpGet("{eventId}")]
    public async Task<EventResponse> GetEvent(Guid eventId)
    {
        var query = new GetEventQuery(eventId);

        EventResponse response = await sender.Send(query);

        return response;
    }
}
