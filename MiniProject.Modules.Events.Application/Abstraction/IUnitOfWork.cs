namespace MiniProject.Modules.Events.Application.Abstraction;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellation = default);
}
