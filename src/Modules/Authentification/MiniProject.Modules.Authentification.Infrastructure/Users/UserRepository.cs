
using MiniProject.Modules.Authentification.Domain.Users;
using MiniProject.Modules.Authentification.Infrastructure.Database;

namespace MiniProject.Modules.Authentification.Infrastructure.Users;
internal sealed class UserRepository(AuthentificationDbContext context) : IUserRepository
{
    public void Insert(User user)
    {
        context.Users.Add(user);
    }
}
