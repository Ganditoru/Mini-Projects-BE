using MediatR;

namespace MiniProject.Modules.Users.Application.Users.Login;
public record LoginCommand(string Email) : IRequest<string?>;
