using MediatR;

namespace MiniProject.Modules.Authentification.Application.Users.GetUser;
public sealed record GetUserQuery(Guid UserId) : IRequest<UserResponse?>;
