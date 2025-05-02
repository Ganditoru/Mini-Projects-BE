using MassTransit;
using MediatR;
using MiniProject.Common.Messaging.Contracts.User;
using MiniProject.Modules.Ticketing.Application.Customers;

namespace MiniProject.Modules.Ticketing.Presentation.Customers;
public sealed class UserRegisteredIntegrationEventConsumer(ISender sender) : IConsumer<UserRegisteredIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        var command = new CreateCustomerCommand(context.Message.UserId, context.Message.Email, context.Message.Name, context.Message.Name);
        
        await sender.Send(command);
    }
}
