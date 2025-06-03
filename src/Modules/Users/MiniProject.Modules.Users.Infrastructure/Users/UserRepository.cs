
using System.Threading;
using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Users.Domain.Users;
using MiniProject.Modules.Users.Infrastructure.Database;

namespace MiniProject.Modules.Users.DomaInfrastructurein.Users;
internal sealed class UserRepository(UserDbContext context) : IUserRepository
{
    public User? FindById(Guid id)
    {
        return context.Users
            .Include(u => u.Notifications)
            .FirstOrDefault(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await context.Users
            .Include(u => u.Notifications)
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken: cancellationToken);
    }

    public void Insert(User user)
    {
        context.Users.Add(user);
    }

    public void Update(User user)
    {
        context.Users.Update(user);
    }
}
