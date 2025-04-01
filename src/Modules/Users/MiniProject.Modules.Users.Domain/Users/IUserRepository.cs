namespace MiniProject.Modules.Users.Domain.Users;
public interface IUserRepository
{
    void Insert(User user);

    User? FindById(Guid id);

    void Update(User user);
}
