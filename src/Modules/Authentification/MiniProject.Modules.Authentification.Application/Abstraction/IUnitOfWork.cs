namespace MiniProject.Modules.Authentification.Application.Abstraction;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
