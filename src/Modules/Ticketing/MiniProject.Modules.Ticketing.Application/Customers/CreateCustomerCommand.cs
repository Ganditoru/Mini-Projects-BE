using MediatR;

namespace MiniProject.Modules.Ticketing.Application.Customers;
public sealed record CreateCustomerCommand(Guid CustomerId, string Email, string FirstName, string LastName) : IRequest<Guid>;
