using MediatR;

namespace MiniProject.Modules.Ticketing.Application.Customers.GetCustomer;
public sealed record GetCustomerQuery(Guid CustomerID): IRequest<CustomerResponse>;
