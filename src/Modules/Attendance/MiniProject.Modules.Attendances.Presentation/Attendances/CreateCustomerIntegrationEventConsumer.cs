
using MassTransit;
using MediatR;
using MiniProject.Common.Messaging.Contracts.IntegrationEvents;
using MiniProject.Modules.Attendance.Application.Abstract.EventBus;
using MiniProject.Modules.Attendance.Application.Attendees.CreateAttendee;

namespace MiniProject.Modules.Attendance.Presentation.Attendances;
public sealed class CreateCustomerIntegrationEventConsumer(ISender sender) : IntegrationEventHandler<CreateCustomerIntegrationEvent>
{
    public override async Task Handle(CreateCustomerIntegrationEvent context, CancellationToken cancellationToken = default)
    {
        await sender.Send(new CreateAttendeeCommand(
            context.CustomerId,
            context.Email,
            context.Name),
            cancellationToken);
    }
}
