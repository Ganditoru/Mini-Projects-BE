using Grpc.Core;
using MiniProject.Modules.Attendance.Presentation.gRPC;
using MiniProjects.Common.Messaging.Contracts.gRPC;

namespace MiniProject.Modules.Attendance.Infrastructure.gRPC;
internal sealed class GrpcCreateEvent(CreateEventEventModuleService.CreateEventEventModuleServiceClient grpc) : IGrpcCreateEvent
{
    public async Task<CreateEventResponse> CreateEventInEventModule(CreateEventRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            CreateEventResponse reply = await grpc.CreateEventAsync(request, cancellationToken: cancellationToken);

            return reply;
        }
        catch (RpcException ex) when(ex.StatusCode == StatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<CompensateEventResponse> CreateEventInEventModule(CompensateEventRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            CompensateEventResponse reply = await grpc.CompensateEventAsync(request, cancellationToken: cancellationToken);

            return reply;
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            return null;
        }
    }
}
