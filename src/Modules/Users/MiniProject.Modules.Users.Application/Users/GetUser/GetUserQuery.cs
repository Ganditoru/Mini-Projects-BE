using MediatR;

namespace MiniProject.Modules.Users.Application.Users.GetUser;
public sealed record GetUserQuery(Guid UserId) : IRequest<UserResponse?>;
