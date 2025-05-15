

using System.Data.Common;
using Dapper;
using MediatR;
using MiniProject.Modules.Ticketing.Application.Abstract;

namespace MiniProject.Modules.Ticketing.Application.Customers.GetCustomer;
internal sealed class GetCustomerByIdQueryHandler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<GetCustomerQuery, CustomerResponse?>
{
    public async Task<CustomerResponse?> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(CustomerResponse.Id)},
                 email AS {nameof(CustomerResponse.Email)},
                 name AS {nameof(CustomerResponse.Name)}
             FROM ticketing2.customers
             WHERE id = @CustomerId
             """;

        CustomerResponse? customer = await connection.QuerySingleOrDefaultAsync<CustomerResponse>(sql, request);

        return customer;
    }
}
