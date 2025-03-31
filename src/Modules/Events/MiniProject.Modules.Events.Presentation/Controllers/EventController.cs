using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject.Modules.Events.Application.Events.CancelEvent;
using MiniProject.Modules.Events.Application.Events.CreateEvent;
using MiniProject.Modules.Events.Application.Events.GetEvent;
using MiniProject.Modules.Events.Application.Events.GetEvents;
using MiniProject.Modules.Events.Application.Events.PublishEvent;
using MiniProject.Modules.Events.Application.Events.RescheduleEvent;
using MiniProject.Modules.Events.Domain.Abstractions;
using MiniProject.Modules.Events.Presentation.Abstraction;
using MiniProject.Modules.Events.Presentation.RequestDTOs;
using MiniProject.Modules.Events.Presentation.RequestDTOs.Events;

namespace MiniProject.Modules.Events.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController(ISender sender) : ControllerBase
{

    [HttpGet("IResult/{id}")]
    public async Task<IResult> GetEvent1(Guid id)
    {

        Result<Application.Events.GetEvent.EventResponse> result = await sender.Send(new GetEventQuery(id));

        return result.IsSuccess ? Results.Ok(result.Value) : ApiResultsMinimalApi.Problem(result);
    }

    [HttpGet("IActionResult/{id}")]
    public async Task<IActionResult> GetEvent2(Guid id)
    {

        Result<Application.Events.GetEvent.EventResponse> result = await sender.Send(new GetEventQuery(id));

        return result.IsSuccess ? Ok(result.Value) : ApiResults.Problem(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {

        Result<IReadOnlyCollection<Application.Events.GetEvents.EventResponse>> result = await sender.Send(new GetEventsQuery());

        return result.IsSuccess ? Ok(result.Value) : ApiResults.Problem(result);
    }

    [HttpPost("IActionResult")]
    public async Task<IActionResult> CreateEvent1([FromBody] CreateEventequest @event)
    {
        var createEventCommand = new CreateEventCommand(@event.CategoryId, @event.Title, @event.Description, @event.Location, @event.StartsAtUtc, @event.EndsAtUtc);

        Result<Guid> result = await sender.Send(createEventCommand);

        return result.IsSuccess ? Ok(result.Value) : ApiResults.Problem(result);
    }

    [HttpPost("IResult")]
    public async Task<IResult> CreateEvent2([FromBody] CreateEventequest @event)
    {
        var createEventCommand = new CreateEventCommand(@event.CategoryId, @event.Title, @event.Description, @event.Location, @event.StartsAtUtc, @event.EndsAtUtc);

        Result<Guid> result = await sender.Send(createEventCommand);

        return result.IsSuccess ? Results.Ok(result.Value) : ApiResultsMinimalApi.Problem(result);
    }

    [HttpPost("{id}/publish")]
    public async Task<IActionResult> PublishEvent(Guid id)
    {
        var publishEventCommand = new PublishEventCommand(id);

        Result result = await sender.Send(publishEventCommand);

        return result.IsSuccess ? NoContent() : ApiResults.Problem(result);
    }

    [HttpPost("{id}/reschedule")]
    public async Task<IActionResult> RescheduleEvent(Guid id, [FromBody] RescheduleEventRequest request)
    {
        var rescheduleEventCommand = new RescheduleEventCommand(id, request.StartsAtUtc, request.EndsAtUtc);

        Result result = await sender.Send(rescheduleEventCommand);

        return result.IsSuccess ? NoContent() : ApiResults.Problem(result);
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> CancelEvent(Guid id)
    {
        var cancelEventCommand = new CancelEventCommand(id);

        Result result = await sender.Send(cancelEventCommand);

        return result.IsSuccess ? NoContent() : ApiResults.Problem(result);
    }
}

