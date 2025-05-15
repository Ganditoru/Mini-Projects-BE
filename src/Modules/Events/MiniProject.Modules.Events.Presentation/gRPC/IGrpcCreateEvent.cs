using MiniProjects.Common.Messaging.Contracts.gRPC;

namespace MiniProject.Modules.Events.Presentation.gRPC;
public interface IGrpcCreateEvent
{
    Task<CreateEventResponse> CreateEventInAttendanceModule(CreateEventRequest request, CancellationToken cancellationToken = default);

    Task<CompensateEventResponse> CompensateEventInAttendanceModule(CompensateEventRequest request, CancellationToken cancellationToken = default);

}
