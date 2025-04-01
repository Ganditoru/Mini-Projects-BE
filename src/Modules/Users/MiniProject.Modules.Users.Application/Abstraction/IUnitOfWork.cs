namespace MiniProject.Modules.Users.Application.Abstraction;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
