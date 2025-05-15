using MassTransit;
using MediatR;
using MiniProject.Common.Messaging.Contracts.IntegrationEvents;
using MiniProject.Modules.Ticketing.Application.Customers.CreateCustomer;
using MiniProject.Modules.Ticketing.Application.Customers.GetCustomer;

namespace MiniProject.Modules.Ticketing.Presentation.Customers;
public sealed class UserRegisteredIntegrationEventConsumer(ISender sender) : IConsumer<UserRegisteredIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        CustomerResponse consummer = await sender.Send(new GetCustomerQuery(context.Message.UserId));

        if (consummer != null)
        {
            return;
        }

        var command = new CreateCustomerCommand(context.Message.UserId, context.Message.Email, context.Message.Name);

        await sender.Send(command, context.CancellationToken);
    }
}
