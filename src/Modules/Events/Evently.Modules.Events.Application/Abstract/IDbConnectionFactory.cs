using System.Data.Common;

namespace Evently.Modules.Events.Application.Abstract;
public  interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
