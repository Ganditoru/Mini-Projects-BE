using MiniProject.Modules.Events.Application.Abstractions;
using MiniProject.Modules.Events.Presentation.gRPC;
using MiniProjects.Common.Messaging.Contracts.gRPC;

namespace MiniProject.Modules.Events.Presentation.Saga;
public sealed class CreateEventInAttendanceModuleService(IGrpcCreateEvent gRPCCreateEvent) : ISagaStep
{
    public async Task<CreateEventResponse> ExecuteAsync(CreateEventRequest request)
    {
        CreateEventResponse result = await gRPCCreateEvent.CreateEventInAttendanceModule(request);

        return result;
    }

    public async Task<CompensateEventResponse> CompensateAsync(CompensateEventRequest request)
    {
        CompensateEventResponse result = await gRPCCreateEvent.CompensateEventInAttendanceModule(request);

        return result;
    }
}
