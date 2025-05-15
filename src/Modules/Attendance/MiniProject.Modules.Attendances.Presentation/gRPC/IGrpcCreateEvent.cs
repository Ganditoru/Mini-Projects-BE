using MiniProjects.Common.Messaging.Contracts.gRPC;

namespace MiniProject.Modules.Attendance.Presentation.gRPC;
public interface IGrpcCreateEvent
{
    Task<CreateEventResponse> CreateEventInEventModule(CreateEventRequest request, CancellationToken cancellationToken = default);

    Task<CompensateEventResponse> CreateEventInEventModule(CompensateEventRequest request, CancellationToken cancellationToken = default);

}
