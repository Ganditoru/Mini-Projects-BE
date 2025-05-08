
namespace MiniProject.Modules.Attendance.Application.Abstract;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
