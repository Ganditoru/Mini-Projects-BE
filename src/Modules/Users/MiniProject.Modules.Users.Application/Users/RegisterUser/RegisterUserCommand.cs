using MediatR;

namespace MiniProject.Modules.Users.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(string Name, string Email, string Password, string UserName) : IRequest<Guid>;

