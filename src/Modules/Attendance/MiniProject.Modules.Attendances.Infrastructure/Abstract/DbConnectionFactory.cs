using System.Data.Common;
using MiniProject.Modules.Attendance.Application.Abstract;
using Npgsql;

namespace MiniProject.Modules.Attendance.Infrastructure.Abstract;
internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}
