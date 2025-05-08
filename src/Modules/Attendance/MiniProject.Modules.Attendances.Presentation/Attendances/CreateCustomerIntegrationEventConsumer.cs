
using MassTransit;
using MediatR;
using MiniProject.Common.Messaging.Contracts.IntegrationEvents;
using MiniProject.Modules.Attendance.Application.Attendees.CreateAttendee;

namespace MiniProject.Modules.Attendance.Presentation.Attendances;
public sealed class CreateCustomerIntegrationEventConsumer(ISender sender) : IConsumer<CreateCustomerIntegrationEvent>
{
    public async Task Consume(ConsumeContext<CreateCustomerIntegrationEvent> context)
    {
        await sender.Send(new CreateAttendeeCommand(
            context.Message.CustomerId,
            context.Message.Email,
            context.Message.Name));
    }
}
