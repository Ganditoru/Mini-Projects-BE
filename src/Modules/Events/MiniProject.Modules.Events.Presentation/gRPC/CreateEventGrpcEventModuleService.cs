
using Grpc.Core;
using MediatR;
using MiniProject.Modules.Events.Application.Events.CompensateEvent;
using MiniProject.Modules.Events.Application.Events.CreateEvent;
using MiniProject.Modules.Events.Domain.Abstractions;
using MiniProjects.Common.Messaging.Contracts.gRPC;

namespace MiniProject.Modules.Events.Presentation.gRPC;
public sealed class CreateEventGrpcEventModuleService(ISender sender) : CreateEventEventModuleService.CreateEventEventModuleServiceBase
{

    public override async Task<CreateEventResponse> CreateEvent(CreateEventRequest request, ServerCallContext context)
    {
        Result<Guid> result = await sender.Send(new CreateEventCommand(
            Guid.Parse(request.CategoryId),
            request.Title,
            request.Description,
            request.Location,
            request.StartsAtUtc.ToDateTime(),
            request.EndsAtUtc.ToDateTime()));

        return new CreateEventResponse { 
            EventId = result.Value.ToString(),
            Success = result.IsSuccess
        };
    }

    public override async Task<CompensateEventResponse> CompensateEvent(
       CompensateEventRequest request,
       ServerCallContext context)
    {
        Result result = await sender.Send(new CompensateEventCommand(Guid.Parse(request.EventId)));

        return new CompensateEventResponse
        {
            Success = result.IsSuccess
        };
    }
}
