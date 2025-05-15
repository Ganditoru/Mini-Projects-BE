using MiniProjects.Common.Messaging.Contracts.gRPC;

namespace MiniProject.Modules.Events.Application.Abstractions;
public interface ISagaStep
{
    Task<CreateEventResponse> ExecuteAsync(CreateEventRequest request);
    Task<CompensateEventResponse> CompensateAsync(CompensateEventRequest request);
}
