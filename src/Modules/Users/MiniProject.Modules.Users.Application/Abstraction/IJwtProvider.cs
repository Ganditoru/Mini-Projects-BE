
using MiniProject.Modules.Users.Domain.Users;

namespace MiniProject.Modules.Users.Application.Abstraction;
public  interface IJwtProvider
{
    string Generate(User user);
}
