using System.Threading;

namespace MiniProject.Modules.Users.Domain.Users;
public interface IUserRepository
{
    void Insert(User user);

    User? FindById(Guid id);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    void Update(User user);
}
