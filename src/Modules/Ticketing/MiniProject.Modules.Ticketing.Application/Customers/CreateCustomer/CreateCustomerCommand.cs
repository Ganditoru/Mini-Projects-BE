using MediatR;

namespace MiniProject.Modules.Ticketing.Application.Customers.CreateCustomer;
public sealed record CreateCustomerCommand(Guid CustomerId, string Email, string Name) : IRequest<Guid>;
