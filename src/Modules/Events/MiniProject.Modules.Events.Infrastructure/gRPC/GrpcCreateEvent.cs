
using Grpc.Core;
using MiniProject.Modules.Events.Presentation.gRPC;
using MiniProjects.Common.Messaging.Contracts.gRPC;

namespace MiniProject.Modules.Events.Infrastructure.gRPC;
internal sealed class GrpcCreateEvent(CreateEventAttendanceModuleService.CreateEventAttendanceModuleServiceClient grpc) : IGrpcCreateEvent
{
    public async Task<CreateEventResponse> CreateEventInAttendanceModule(CreateEventRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            CreateEventResponse reply = await grpc.CreateEventAsync(request, cancellationToken: cancellationToken);

            return reply;
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error at Creating Event in Attendance module {exception}");
            return null;
        }
    }

    public async Task<CompensateEventResponse> CompensateEventInAttendanceModule(CompensateEventRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            CompensateEventResponse reply = await grpc.CompensateEventAsync(request, cancellationToken: cancellationToken);

            return reply;
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error at Compensating the Event creation in Attendance module {exception}");
            return null;
        }
    }
}
