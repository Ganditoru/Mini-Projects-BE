using MediatR;
using MiniProject.Modules.Ticketing.Application.Abstract;
using MiniProject.Modules.Ticketing.Domain.Customers;

namespace MiniProject.Modules.Ticketing.Application.Customers;

internal sealed class CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateCustomerCommand, Guid>
{
    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(request.CustomerId, request.Email, request.FirstName, request.LastName);
        customerRepository.Insert(customer);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}
