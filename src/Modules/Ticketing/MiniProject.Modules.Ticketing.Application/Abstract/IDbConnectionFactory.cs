
using System.Data.Common;

namespace MiniProject.Modules.Ticketing.Application.Abstract;
public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
