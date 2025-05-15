using MediatR;
using MiniProject.Modules.Events.Application.Abstractions;
using MiniProject.Modules.Events.Application.Events.CompensateEvent;
using MiniProject.Modules.Events.Application.Events.CreateEvent;
using MiniProject.Modules.Events.Domain.Abstractions;
using MiniProjects.Common.Messaging.Contracts.gRPC;

namespace MiniProject.Modules.Events.Presentation.Saga;
public sealed class CreateEventInEventModuleService(ISender sender) : ISagaStep
{
    public async Task<CreateEventResponse> ExecuteAsync(CreateEventRequest request)
    {
        Result<Guid> result = await sender.Send(new CreateEventCommand(
             Guid.Parse(request.CategoryId),
             request.Title,
             request.Description,
             request.Location,
             request.StartsAtUtc.ToDateTime(),
             request.EndsAtUtc.ToDateTime()));

        return new CreateEventResponse
        {
            EventId = result.Value.ToString(),
            Success = result.IsSuccess
        };
    }

    public async Task<CompensateEventResponse> CompensateAsync(CompensateEventRequest request)
    {
        Result result = await sender.Send(new CompensateEventCommand(Guid.Parse(request.EventId)));

        return new CompensateEventResponse
        {
            Success = result.IsSuccess
        };
    }
}
