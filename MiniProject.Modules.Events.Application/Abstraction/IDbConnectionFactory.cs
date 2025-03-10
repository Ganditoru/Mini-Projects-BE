using System.Data.Common;

namespace MiniProject.Modules.Events.Application.Abstraction;
public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}

