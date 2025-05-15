
using System.Data.Common;

namespace MiniProject.Modules.Attendance.Application.Abstract;
public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}

