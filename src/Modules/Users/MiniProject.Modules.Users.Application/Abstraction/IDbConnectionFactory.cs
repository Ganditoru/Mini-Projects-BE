using System.Data.Common;

namespace MiniProject.Modules.Events.Application.Abstractions.Data;
public  interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
