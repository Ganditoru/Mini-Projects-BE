
using Grpc.Core;
using MediatR;
using MiniProject.Modules.Attendance.Application.Events.CompensateEventCommand;
using MiniProject.Modules.Attendance.Application.Events.CreateEvent;
using MiniProject.Modules.Attendance.Domain.Abstract;
using MiniProjects.Common.Messaging.Contracts.gRPC;

namespace MiniProject.Modules.Attendance.Presentation.gRPC;
public sealed class CreateEventGrpcAttendanceModuleService(ISender sender)
    : CreateEventAttendanceModuleService.CreateEventAttendanceModuleServiceBase
{
    public override async Task<CreateEventResponse> CreateEvent(CreateEventRequest request, ServerCallContext context)
    {
        Result<Guid> result = await sender.Send(new CreateEventCommand(
            request.Title,
            request.Description,
            request.Location,
            request.StartsAtUtc.ToDateTime(),
            request.EndsAtUtc.ToDateTime()));

        return new CreateEventResponse
        {
            EventId = result.IsSuccess ? result.Value.ToString() : string.Empty,
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
