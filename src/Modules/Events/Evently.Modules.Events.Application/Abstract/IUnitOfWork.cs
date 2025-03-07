namespace Evently.Modules.Events.Application.Abstract;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
