
using MiniProject.Modules.Ticketing.Domain.Abstract;

namespace MiniProject.Modules.Ticketing.Domain.Customers;
public sealed class Customer : Entity
{
    private Customer(){ }

    public Guid Id { get; init; }

    public string Email { get; init; }

    public string Name { get; private set; }

    public static Customer Create(Guid id, string email, string name)
    {
        var customer = new Customer { Id = id, Email = email, Name = name };

        customer.Raise(new CreateCustomerDomainEvent(customer.Id));
        return customer;
    }

    public void Update(string name)
    {
        Name = name;
    }
}
