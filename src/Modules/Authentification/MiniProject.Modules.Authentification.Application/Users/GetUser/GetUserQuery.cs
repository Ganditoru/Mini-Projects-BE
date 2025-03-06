using MediatR;
using MiniProject.Modules.Authentification.Application.Users.Models;

namespace MiniProject.Modules.Authentification.Application.Users.GetUser;
public sealed record GetUserQuery(Guid UserId) : IRequest<UserResponse?>;
