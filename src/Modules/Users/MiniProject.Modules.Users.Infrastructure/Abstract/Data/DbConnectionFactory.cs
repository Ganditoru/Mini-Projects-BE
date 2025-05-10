using System.Data.Common;
using MiniProject.Modules.Events.Application.Abstractions.Data;
using Npgsql;

namespace MiniProject.Modules.Users.Infrastructure.Abstract.Data;
internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{

    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}
