using System.Data.Common;
using MiniProject.Modules.Events.Application.Abstractions;
using Npgsql;

namespace MiniProject.Modules.Events.Infrastructure.Data;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}
