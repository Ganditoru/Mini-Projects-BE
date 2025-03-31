namespace MiniProject.Modules.Authentification.Application.Users;

public sealed record UserResponse(
    Guid Id,
    string Name,
    string Email,
    string UserName,
    string Password);
