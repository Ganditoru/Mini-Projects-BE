using System.Data.Common;

namespace MiniProject.Modules.Events.Application.Abstractions;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
